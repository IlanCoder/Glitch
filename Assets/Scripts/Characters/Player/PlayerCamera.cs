using UnityEngine;

namespace Characters.Player {
    public class PlayerCamera : MonoBehaviour {
        public Camera cam;
        [HideInInspector] public PlayerManager player;
        [HideInInspector] public PlayerInputManager inputManager;
        [SerializeField] Transform pivot;

        [Header("Camera Settings")]
        [SerializeField, Min(0.1f)] float camSmoothTime = 1;
        [SerializeField] float yAxisSpeed = 220;
        [SerializeField] float xAxisSpeed = 160;
        [SerializeField] float minimumPivot = -60;
        [SerializeField] float maximumPivot = 30;
        [SerializeField] float camCollisionRadius = 0.2f;
        [SerializeField] LayerMask camCollisionLayer;
        
        [Header("Lock On")]
        [SerializeField, Min(0.1f)] float lockOnSmoothSpeed = 1;
        [SerializeField] float maxLockOnDistance = 100f;
        [SerializeField] float lockOnCamHeight = 0.5f;
        [SerializeField, Min(0.1f)] float camHeightSmoothSpeed = 0.05f;
        [SerializeField] LayerMask lockOnLayer;
        [SerializeField] LayerMask lockOnObstructLayer;

        Vector3 _camVelocity = Vector3.zero;
        float _yAxisAngle;
        float _xAxisAngle;
        float _camZPosition;
        float _targetCamZPos;

        float _clampLerpPivot;
        bool _lerpToClamp;
        
        void Awake() {
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            _camZPosition = cam.transform.localPosition.z;
        }

        public void HandleCamera() {
            HandleFollowPlayer();
            if (player.isLockedOn) {
                HandleLockedRotation();
                CheckIfTargetIsInRange();
            } else {
                HandleUnlockedRotation();
            }
            HandleCollision();  
        }

        void HandleFollowPlayer() {
            Vector3 targetCameraPos = Vector3.SmoothDamp(transform.position, player.transform.position,
                ref _camVelocity, camSmoothTime);
            transform.position = targetCameraPos;
        }

        void HandleLockedRotation() {
            Vector3 lockOnPos = player.combatManager.LockOnTarget.CombatManager.LockOnPivot.position;
            Vector3 rotationDirection = lockOnPos - pivot.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);
            rotationDirection = targetRotation.eulerAngles;

            targetRotation = Quaternion.Euler(new Vector3(0, rotationDirection.y));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lockOnSmoothSpeed);

            targetRotation = Quaternion.Euler(new Vector3(rotationDirection.x, 0));
            pivot.localRotation = Quaternion.Slerp(pivot.transform.localRotation, targetRotation, lockOnSmoothSpeed);
        }

        void CheckIfTargetIsInRange() {
            float distToTarget = Vector3.Distance(player.combatManager.LockOnTarget.transform.position,
                player.transform.position);
            if (distToTarget <= maxLockOnDistance) return;
            player.DisableLockOn();
        }

        void HandleUnlockedRotation() {
            if (!_lerpToClamp) {
                _xAxisAngle -= inputManager.CameraInput.y * xAxisSpeed * Time.deltaTime;
                _xAxisAngle = Mathf.Clamp(_xAxisAngle, minimumPivot, maximumPivot);
            } else {
                LerpToClamp();
            }
            _yAxisAngle += inputManager.CameraInput.x * yAxisSpeed * Time.deltaTime;

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
            Vector3 targetPos = Vector3.zero;
            if (player.isLockedOn) {
                targetPos.y = lockOnCamHeight;
            }
            targetPos.z = _targetCamZPos;
            targetPos.y = Mathf.Lerp(cam.transform.localPosition.y, targetPos.y, camHeightSmoothSpeed);
            targetPos.z = Mathf.Lerp(cam.transform.localPosition.z, targetPos.z, 0.2f);
            cam.transform.localPosition = targetPos;
        }

        void LerpToClamp() {
            float distance = Mathf.Abs(_xAxisAngle - _clampLerpPivot);
            if (distance >= 0.1f) {
                _xAxisAngle = Mathf.LerpAngle(_xAxisAngle, _clampLerpPivot, camSmoothTime);
                return;
            }
            _lerpToClamp = false;
        }

        public void UnlockCamera() {
            _yAxisAngle = transform.rotation.eulerAngles.y;
            _xAxisAngle = pivot.transform.localRotation.eulerAngles.x;
            if (_xAxisAngle > 180) _xAxisAngle -= 360;
            if (_xAxisAngle < minimumPivot) {
                _clampLerpPivot = minimumPivot;
                _lerpToClamp = true;
            }else if (_xAxisAngle > maximumPivot) {
                _clampLerpPivot = maximumPivot;
                _lerpToClamp = true;
            }
        }

        #region Lock-On Targeting
        public bool FindClosestLockOnTarget(out CharacterManager closestTarget) {
            float shortestDistance = maxLockOnDistance;
            closestTarget = null;
            
            Collider[] colliders = Physics.OverlapSphere(player.transform.position, maxLockOnDistance, lockOnLayer);
            
            foreach (Collider col in colliders) {
                if (!CanBeTargeted(col, out CharacterManager target)) continue;

                float distanceToTarget = Vector3.Distance(player.transform.position, target.transform.position);
                
                if (distanceToTarget > shortestDistance) continue;
                shortestDistance = distanceToTarget;
                closestTarget = target;
            }
            return closestTarget != null;
        }

        public bool FindClosestRightLockOnTarget(out CharacterManager closestTarget) {
            float distanceToTarget = maxLockOnDistance;
            float currentTargetRelativePos = cam.transform
                .InverseTransformPoint(player.combatManager.LockOnTarget.transform.position).x;
            closestTarget = null;

            Collider[] colliders = Physics.OverlapSphere(player.transform.position, maxLockOnDistance, lockOnLayer);

            foreach (Collider col in colliders) {
                if (!CanBeTargeted(col, out CharacterManager target)) continue;
                
                float relativeTargetPos = cam.transform.InverseTransformPoint(target.transform.position).x;
                relativeTargetPos -= currentTargetRelativePos;

                if (relativeTargetPos <= 0) continue;
                if (relativeTargetPos >= distanceToTarget) continue;
                distanceToTarget = relativeTargetPos;
                closestTarget = target;
            }
            return closestTarget != null;
        }
        
        public bool FindClosestLeftLockOnTarget(out CharacterManager closestTarget) {
            float distanceToTarget = -maxLockOnDistance;
            float currentTargetRelativePos = cam.transform
                .InverseTransformPoint(player.combatManager.LockOnTarget.transform.position).x;
            closestTarget = null;

            Collider[] colliders = Physics.OverlapSphere(player.transform.position, maxLockOnDistance, lockOnLayer);

            foreach (Collider col in colliders) {
                if (!CanBeTargeted(col, out CharacterManager target)) continue;
                
                float relativeTargetPos = cam.transform.InverseTransformPoint(target.transform.position).x;
                relativeTargetPos -= currentTargetRelativePos;

                if (relativeTargetPos >= 0) continue;
                if (relativeTargetPos <= distanceToTarget) continue;
                distanceToTarget = relativeTargetPos;
                closestTarget = target;
            }
            return closestTarget != null;
        }
        
        bool CanBeTargeted(Collider col, out CharacterManager target) {
            if (!col.TryGetComponent(out target)) return false;
            if (target.isDead) return false;
            if (target == player.combatManager.LockOnTarget) return false;

            Vector3 targetDirection = target.transform.position - cam.transform.position;
            float angleToTarget = Vector3.Angle(cam.transform.forward, targetDirection);

            if (angleToTarget > cam.fieldOfView) return false;
            return !Physics.Linecast(cam.transform.position, target.CombatManager.LockOnPivot.position,
            lockOnObstructLayer);
        }
#endregion
    }
}
