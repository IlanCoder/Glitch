using UnityEngine;

namespace Characters.Player {
    public class PlayerMovementController : CharacterMovementController {
        PlayerManager _playerManager;
        [HideInInspector] public PlayerCamera playerCam;

        [Header("Speeds")]
        [SerializeField] float walkingSpeed = 2;
        [SerializeField] float runningSpeed = 4;
        [SerializeField] float sprintSpeed = 6;
        [SerializeField] float rotationSpeed = 10;
        [SerializeField] float jumpSpeed = 5;
        [SerializeField] float freeFallSpeed = 1;

        [Header("Stamina Costs")] 
        [SerializeField, Min(0.1f)] float sprintCost;
        [SerializeField, Min(1)] int rollCost;
        [SerializeField, Min(1)] int jumpCost;
        
        [Header("Jump")] 
        [SerializeField] float jumpHeight = 2;
        Vector3 _jumpDirection;

        Vector3 _dodgeDirection;
        
        Transform _camManagerTransform;
        Transform _camObjTransform;
        Vector3 _moveDirection;
        Vector3 _targetRotation;
        Vector2 _inputMovement;

        protected override void Awake() {
            base.Awake();
            _playerManager = GetComponent<PlayerManager>();
        }

        public void HandleMovement() {
            if (_playerManager.isDead) return;
            GetCameraTransforms();
            HandleGroundMovement();
            HandleJumpMovement();
            HandleRotation();
            HandleFreeFallMovement();
        }

        void GetCameraTransforms() {
            _camManagerTransform = playerCam.transform;
            _camObjTransform = playerCam.cam.transform;
        }

        void HandleGroundMovement() {
            if (_playerManager.movementLocked) return;
            _inputMovement = _playerManager.inputController.MovementInput;
            if (_playerManager.isLockedOn && !_playerManager.isSprinting) {
                _playerManager.animController.UpdateMovementParameters(_inputMovement.x, _inputMovement.y);
                _moveDirection = GetNormalizedHorizontalDirection(transform);
            } else {
                _moveDirection = GetNormalizedHorizontalDirection(_camManagerTransform);
                _playerManager.animController.UpdateMovementParameters(0, _playerManager.inputController.MoveAmount);
                if(_playerManager.isSprinting) _playerManager.statsController.UseStamina(sprintCost * Time.deltaTime);
            }
            _playerManager.characterController.Move(GetGroundSpeed() * Time.deltaTime * _moveDirection);
        }

        void HandleJumpMovement() {
            if (!_playerManager.isJumping) return;
            _playerManager.characterController.Move(jumpSpeed * Time.deltaTime * _jumpDirection);
        }

        void HandleFreeFallMovement() {
            if (_playerManager.isGrounded) return;
            Vector3 freeFallDirection = GetNormalizedHorizontalDirection(_camManagerTransform);
            _playerManager.characterController.Move(freeFallSpeed * Time.deltaTime * freeFallDirection);
        }

        void HandleRotation() {
            if (_playerManager.rotationLocked) return;
            if (_playerManager.isLockedOn && !_playerManager.isSprinting) {
                HandleLockedRotation();
                return;
            }
            HandleUnlockedRotation();
        }

        void HandleLockedRotation() {
            Vector3 targetDirection = _playerManager.combatController.LockOnTarget.transform.position - transform.position;
            targetDirection.y = 0;
            targetDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        void HandleUnlockedRotation() {
            CheckRotationRelativeToCam();
            if (_targetRotation == Vector3.zero) return;
            
            Quaternion newRotation = Quaternion.LookRotation(_targetRotation);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }

        public void HandleSprint() {
            if (!_playerManager.statsController.CanPerformStaminaAction() || 
                _playerManager.inputController.MoveAmount < 0.5f) {
                _playerManager.isSprinting = false;
                return;
            }
            _playerManager.isSprinting = true;
        }
        
        void CheckRotationRelativeToCam() {
            _targetRotation = _camObjTransform.forward * _inputMovement.y + _camObjTransform.right * _inputMovement.x;
            _targetRotation.y = 0;
            _targetRotation.Normalize();
        }

        public void AttemptToDodge() {
            if (!_playerManager.statsController.CanPerformStaminaAction()) return;
            if (_playerManager.inputController.MoveAmount > 0) {
                CheckRotationRelativeToCam();
                Quaternion newRotation = Quaternion.LookRotation(_targetRotation);
                transform.rotation = newRotation;
                _playerManager.animController.PlayDodgeAnimation();
                _playerManager.statsController.UseStamina(rollCost);
                return;
            }
            _playerManager.animController.PlayDodgeAnimation(true);
            _playerManager.statsController.UseStamina(rollCost);
        }

        public void AttemptToJump() {
            if (!_playerManager.statsController.CanPerformStaminaAction()) return;
            if(_playerManager.isJumping) return;
            if (!_playerManager.isGrounded) return;
            _playerManager.isJumping = true;
            _playerManager.animController.PlayJumpAnimation();
            _playerManager.statsController.UseStamina(jumpCost);
            _jumpDirection = GetNormalizedHorizontalDirection(_camManagerTransform);
            _jumpDirection *= GetJumpSpeed();
        }

        float GetJumpSpeed() {
            if (_playerManager.isSprinting) return 1;
            switch (_playerManager.inputController.MoveAmount) {
                case 0.5f:
                    return 0.25f;
                case 1:
                    return 0.5f;
                default: return 0;
            }
        }
        
        float GetGroundSpeed() {
            if (_playerManager.isSprinting) return sprintSpeed;
            switch (_playerManager.inputController.MoveAmount) {
                case 0.5f:
                    return walkingSpeed;
                case 1:
                    return runningSpeed;
                default: return 0;
            }
        }

        Vector3 GetNormalizedHorizontalDirection(Transform guideTransform) {
            Vector3 direction = guideTransform.forward * _inputMovement.y + 
                                guideTransform.right * _inputMovement.x;
            direction.y = 0;
            direction.Normalize();
            return direction;
        }

        #region Animation Events
        public void ApplyJumpingVelocity() {
            yVel.y = Mathf.Sqrt(jumpHeight * -2 * gravityForce);
            _playerManager.animController.ApplyRootMotion(false);
        }
        #endregion
    }
}
