using UnityEngine;

namespace Characters.Player {
    public class PlayerMovementManager : CharacterMovementManager<PlayerManager> {
        [HideInInspector] public PlayerCamera playerCam;

        [Header("Speeds")]
        [SerializeField] float walkingSpeed = 2;
        [SerializeField] float runningSpeed = 4;
        [SerializeField] float sprintSpeed = 6;
        [SerializeField] float rotationSpeed = 10;

        [Header("Stamina Costs")] 
        [SerializeField, Min(0.1f)] float sprintCost;
        [SerializeField, Min(1)] int rollCost;
        [SerializeField, Min(1)] int backStepCost;
        [SerializeField, Min(1)] int jumpCost;
        
        Transform _camManagerTransform;
        Transform _camObjTransform;
        Vector3 _moveDirection;
        Vector3 _targetRotation;
        Vector2 _inputMovement;

        Vector3 _dodgeDirection;
        
        public void HandleMovement() {
            GetCameraTransforms();
            HandleGroundMovement();
            HandleRotation();
        }

        void GetCameraTransforms() {
            _camManagerTransform = playerCam.transform;
            _camObjTransform = playerCam.cam.transform;
        }

        void HandleGroundMovement() {
            if (manager.movementLocked) return;
            _inputMovement = manager.inputManager.MovementInput;
            _moveDirection = _camManagerTransform.forward * _inputMovement.y + 
                             _camManagerTransform.right * _inputMovement.x;
            _moveDirection.y = 0;
            _moveDirection.Normalize();
            manager.controller.Move(GetGroundSpeed() * Time.deltaTime * _moveDirection);
            manager.animManager.UpdateMovementParameters(0, manager.inputManager.MoveAmount);
        }

        float GetGroundSpeed()
        {
            if (manager.isSprinting) return sprintSpeed;
            switch (manager.inputManager.MoveAmount) {
                case 0.5f:
                    return walkingSpeed;
                case 1:
                    return runningSpeed;
                default: return 0;
            }
        }

        void HandleRotation() {
            if (manager.rotationLocked) return;
            CheckRotationRelativeToCam();
            if (_targetRotation == Vector3.zero) return;
            Quaternion newRotation = Quaternion.LookRotation(_targetRotation);
            Quaternion targetRotation =
                Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }

        void CheckRotationRelativeToCam() {
            _targetRotation = _camObjTransform.forward * _inputMovement.y + _camObjTransform.right * _inputMovement.x;
            _targetRotation.y = 0;
            _targetRotation.Normalize();
        }

        public void AttemptToDodge() {
            if (!CanPerformStaminaAction()) return;
            if (manager.inputManager.MoveAmount > 0) {
                CheckRotationRelativeToCam();
                Quaternion newRotation = Quaternion.LookRotation(_targetRotation);
                transform.rotation = newRotation;
                manager.animManager.PlayTargetAnimation("Dodge_F", false);
                manager.statManager.UseStamina(rollCost);
                return;
            }
            manager.animManager.PlayTargetAnimation("Dodge_B", false);
            manager.statManager.UseStamina(backStepCost);
        }

        public void AttemptToJump() {
            if (!CanPerformStaminaAction()) return;
            if(manager.isJumping) return;
            if (!manager.isGrounded) return;
            
            manager.animManager.PlayTargetAnimation("Jump_Start",true);
            manager.statManager.UseStamina(jumpCost);
            manager.isJumping = true;
        }

        public void HandleSprint() {
            if (!CanPerformStaminaAction() || 
                manager.inputManager.MoveAmount < 0.5f) {
                manager.isSprinting = false;
                return;
            }
            manager.isSprinting = true;
            manager.statManager.UseStamina(sprintCost * Time.deltaTime);
        }

        bool CanPerformStaminaAction() {
            if (manager.isPerformingAction) return false;
            return manager.statManager.CurrentStamina > 0;
        }

        public void ApplyJumpingVelocity() {
            
        }
    }
}
