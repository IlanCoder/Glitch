using System;
using Unity.Mathematics;
using UnityEngine;

namespace Characters.Player {
    public class PlayerCamera : MonoBehaviour {
        public Camera cam;
        [HideInInspector] public PlayerManager player;
        [HideInInspector] public PlayerInputManager inputManager;
        [SerializeField] Transform pivot;

        [Header("Camera Settings")]
        [SerializeField] float camSmoothTime = 1;
        [SerializeField] float yAxisSpeed = 220;
        [SerializeField] float xAxisSpeed = 160;
        [SerializeField] float minimumPivot = -60;
        [SerializeField] float maximumPivot = 30;
        [SerializeField] float camCollisionRadius = 0.2f;
        [SerializeField] LayerMask camCollisionLayer;
        
        [Header("Lock On")]
        [SerializeField] float lockOnSmoothSpeed = 1;
        [SerializeField] float maxLockOnDistance = 100f;
        [SerializeField] LayerMask lockOnLayer;
        [SerializeField] LayerMask lockOnObstructLayer;
        
        Vector3 _camVelocity = Vector3.zero;
        Vector3 _camPosition;
        float _yAxisAngle = 0;
        float _xAxisAngle = 0;
        float _camZPosition;
        float _targetCamZPos;
        
        void Awake() {
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            _camZPosition = cam.transform.localPosition.z;
        }

        public void HandleCamera() {
            HandleFollowPlayer();
            HandleRotation();
            HandleCollision();  
        }

        void HandleFollowPlayer() {
            Vector3 targetCameraPos = Vector3.SmoothDamp(transform.position, player.transform.position,
                ref _camVelocity, camSmoothTime);
            transform.position = targetCameraPos;
        }

        void HandleRotation() {
            if (player.isLockedOn) {
                HandleLockedRotation();
                return;
            }
            HandleUnlockedRotation();
        }

        void HandleLockedRotation() {
            Vector3 lockOnPos = player.combatManager.LockOnTarget.CombatManager.LockOnPivot.position;
            Vector3 rotationDirection = lockOnPos - transform.position;
            rotationDirection.y = 0;
            rotationDirection.Normalize();
   
            Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lockOnSmoothSpeed);

            rotationDirection = lockOnPos - pivot.transform.position;
            rotationDirection.Normalize();

            targetRotation = Quaternion.LookRotation(rotationDirection);
            pivot.transform.rotation = Quaternion.Slerp(pivot.transform.rotation, targetRotation, lockOnSmoothSpeed);
            
            _xAxisAngle = pivot.transform.rotation.x;
            _yAxisAngle = transform.rotation.y;
        }

        void HandleUnlockedRotation() {
            _yAxisAngle += inputManager.CameraInput.x * yAxisSpeed * Time.deltaTime;
            _xAxisAngle -= inputManager.CameraInput.y * xAxisSpeed * Time.deltaTime;
            _xAxisAngle = Mathf.Clamp(_xAxisAngle, minimumPivot, maximumPivot);
            
            transform.rotation = Quaternion.Euler(new Vector3(0, _yAxisAngle));
            
            pivot.localRotation = Quaternion.Euler(new Vector3(_xAxisAngle, 0));
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

        public void FindLockOnTargets() {
            float shortestDistance = maxLockOnDistance;
            float angleToTarget;
            float distanceToTarget;
            Vector3 targetDirection= Vector3.zero;
            
            Collider[] colliders = Physics.OverlapSphere(player.transform.position, maxLockOnDistance, lockOnLayer);

            foreach (Collider col in colliders) {
                if (!col.TryGetComponent(out CharacterManager target)) continue;
                if (target.isDead) continue;
                
                targetDirection = target.transform.position - player.transform.position;
                angleToTarget = Vector3.Angle(cam.transform.forward, targetDirection);
                
                if (angleToTarget > cam.fieldOfView) continue;
                if (Physics.Linecast(cam.transform.position, target.CombatManager.LockOnPivot.position,
                    lockOnObstructLayer)) continue;
                
                distanceToTarget = Vector3.Distance(player.transform.position, target.transform.position);
                
                if (distanceToTarget > shortestDistance) continue;
                shortestDistance = distanceToTarget;
                ChangeTarget(target);
            }
        }

        void ChangeTarget(CharacterManager newTarget) {
            player.combatManager.ChangeTarget(newTarget);
        }
    }
}
