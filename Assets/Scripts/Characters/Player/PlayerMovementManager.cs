using UnityEngine;

namespace Characters.Player {
    public class PlayerMovementManager : CharacterMovementManager<PlayerManager> {
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
        
        public void HandleMovement() {
            if (manager.isDead) return;
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
            if (manager.movementLocked) return;
            _inputMovement = manager.inputManager.MovementInput;
            _moveDirection = GetNormalizedHorizontalDirection();
            manager.controller.Move(GetGroundSpeed() * Time.deltaTime * _moveDirection);
            if (manager.isLockedOn && !manager.isSprinting) {
                manager.animManager.UpdateMovementParameters(_inputMovement.x, _inputMovement.y);
                return;
            }
            manager.animManager.UpdateMovementParameters(0, manager.inputManager.MoveAmount);
        }

        void HandleJumpMovement() {
            if (!manager.isJumping) return;
            manager.controller.Move(jumpSpeed * Time.deltaTime * _jumpDirection);
        }

        void HandleFreeFallMovement() {
            if (manager.isGrounded) return;
            Vector3 freeFallDirection = GetNormalizedHorizontalDirection();
            manager.controller.Move(freeFallSpeed * Time.deltaTime * freeFallDirection);
        }

        void HandleRotation() {
            if (manager.rotationLocked) return;
            if (manager.isLockedOn && !manager.isSprinting) {
                HandleLockedRotation();
                return;
            }
            HandleUnlockedRotation();
        }

        void HandleLockedRotation() {
            Vector3 targetDirection = manager.combatManager.LockOnTarget.transform.position - transform.position;
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
            if (!manager.statsManager.CanPerformStaminaAction() || 
                manager.inputManager.MoveAmount < 0.5f) {
                manager.isSprinting = false;
                return;
            }
            manager.isSprinting = true;
            manager.statsManager.UseStamina(sprintCost * Time.deltaTime);
        }
        
        void CheckRotationRelativeToCam() {
            _targetRotation = _camObjTransform.forward * _inputMovement.y + _camObjTransform.right * _inputMovement.x;
            _targetRotation.y = 0;
            _targetRotation.Normalize();
        }

        public void AttemptToDodge() {
            if (!manager.statsManager.CanPerformStaminaAction()) return;
            if (manager.inputManager.MoveAmount > 0) {
                CheckRotationRelativeToCam();
                Quaternion newRotation = Quaternion.LookRotation(_targetRotation);
                transform.rotation = newRotation;
                manager.animManager.PlayDodgeAnimation();
                manager.statsManager.UseStamina(rollCost);
                return;
            }
            manager.animManager.PlayDodgeAnimation(true);
            manager.statsManager.UseStamina(rollCost);
        }

        public void AttemptToJump() {
            if (!manager.statsManager.CanPerformStaminaAction()) return;
            if(manager.isJumping) return;
            if (!manager.isGrounded) return;
            manager.isJumping = true;
            manager.animManager.PlayJumpAnimation();
            manager.statsManager.UseStamina(jumpCost);
            _jumpDirection = GetNormalizedHorizontalDirection();
            _jumpDirection *= GetJumpSpeed();
        }

        float GetJumpSpeed() {
            if (manager.isSprinting) return 1;
            switch (manager.inputManager.MoveAmount) {
                case 0.5f:
                    return 0.25f;
                case 1:
                    return 0.5f;
                default: return 0;
            }
        }
        
        float GetGroundSpeed() {
            if (manager.isSprinting) return sprintSpeed;
            switch (manager.inputManager.MoveAmount) {
                case 0.5f:
                    return walkingSpeed;
                case 1:
                    return runningSpeed;
                default: return 0;
            }
        }

        Vector3 GetNormalizedHorizontalDirection() {
            Vector3 direction = _camManagerTransform.forward * _inputMovement.y + 
                             _camManagerTransform.right * _inputMovement.x;
            direction.y = 0;
            direction.Normalize();
            return direction;
        }

        #region Animation Events
        public void ApplyJumpingVelocity() {
            yVel.y = Mathf.Sqrt(jumpHeight * -2 * gravityForce);
        }
        #endregion
    }
}
