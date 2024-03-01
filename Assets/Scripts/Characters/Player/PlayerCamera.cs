using System;
using UnityEngine;

namespace Characters.Player {
    public class PlayerCamera : MonoBehaviour {
        public Camera cam;
        [HideInInspector] public PlayerManager player;
        [HideInInspector] public PlayerInputManager inputManager;
        [SerializeField] Transform pivot;

        [Header("Camera Settings")]
        [SerializeField] float camSmoothSpeed = 1;
        [SerializeField] float yAxisSpeed = 220;
        [SerializeField] float xAxisSpeed = 160;
        [SerializeField] float minimumPivot = -60;
        [SerializeField] float maximumPivot = 30;
        [SerializeField] float camCollisionRadius = 0.2f;
        [SerializeField] LayerMask camCollisionLayer;
        
        Vector3 _camVelocity;
        Vector3 _camPosition;
        float _yAxisAngle = 0;
        float _xAxisAngle = 0;
        float _camZPosition;
        float _targetCamZPos;
        
        void Awake() {
            DontDestroyOnLoad(gameObject);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _camZPosition = cam.transform.localPosition.z;
        }

        public void HandleCamera() {
            HandleFollowPlayer();
            HandleRotation();
            HandleCollision();  
        }

        void HandleFollowPlayer() {
            Vector3 targetCameraPos = Vector3.SmoothDamp(transform.position, player.transform.position,
                ref _camVelocity, camSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPos;
        }

        void HandleRotation() {
            _yAxisAngle += inputManager.CameraInput.x * yAxisSpeed * Time.deltaTime;
            _xAxisAngle -= inputManager.CameraInput.y * xAxisSpeed * Time.deltaTime;
            _xAxisAngle = Mathf.Clamp(_xAxisAngle, minimumPivot, maximumPivot);

            Vector3 camRotation = new Vector3(0, _yAxisAngle);
            Quaternion targetRotation = Quaternion.Euler(camRotation);
            transform.rotation = targetRotation;
            
            camRotation = new Vector3(_xAxisAngle, 0);
            targetRotation = Quaternion.Euler(camRotation);
            pivot.localRotation = targetRotation;

            //transform.LookAt(pivot);
        }

        void HandleCollision() {
            _targetCamZPos = _camZPosition;
            Vector3 direction = -cam.transform.forward.normalized;
            if (Physics.SphereCast(pivot.position, camCollisionRadius, direction, out RaycastHit hit,
                Mathf.Abs(_camZPosition), camCollisionLayer)) {
                float hitObjectDistance = Vector3.Distance(pivot.position, hit.point);
                _targetCamZPos = -(hitObjectDistance - camCollisionRadius);
            }
            if (Mathf.Abs(_targetCamZPos) < camCollisionRadius) {
                  _targetCamZPos = -camCollisionRadius;
            }

            _camPosition.z = Mathf.Lerp(cam.transform.localPosition.z, _targetCamZPos, 0.2f);
            cam.transform.localPosition = _camPosition;
        }
    }
}
