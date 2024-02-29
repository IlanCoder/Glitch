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
        
        Vector3 _cameraVelocity;
        float yAxisAngle = 0;
        float xAxisAngle = 0;
        
        void Awake() {
            DontDestroyOnLoad(gameObject);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void HandleCamera() {
            HandleFollowPlayer();
            HandleRotation();
        }

        void HandleFollowPlayer() {
            Vector3 targetCameraPos = Vector3.SmoothDamp(transform.position, player.transform.position,
                ref _cameraVelocity, camSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPos;
        }

        void HandleRotation() {
            yAxisAngle += inputManager.CameraInput.x * yAxisSpeed * Time.deltaTime;
            xAxisAngle -= inputManager.CameraInput.y * xAxisSpeed * Time.deltaTime;
            xAxisAngle = Mathf.Clamp(xAxisAngle, minimumPivot, maximumPivot);

            Vector3 camRotation = new Vector3(0, yAxisAngle);
            Quaternion targetRotation = Quaternion.Euler(camRotation);
            transform.rotation = targetRotation;
            
            camRotation = new Vector3(xAxisAngle, 0);
            targetRotation = Quaternion.Euler(camRotation);
            pivot.localRotation = targetRotation;

            //transform.LookAt(pivot);
        }
    }
}
