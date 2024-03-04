using UnityEngine;

namespace Characters.Player {
    public class PlayerMovementManager : CharacterMovementManager {
        [HideInInspector]public PlayerCamera playerCam;
        PlayerManager _playerManager;

        [Header("Speeds")]
        [SerializeField] float walkingSpeed = 2;
        [SerializeField] float runningSpeed = 4;
        [SerializeField] float sprintSpeed = 6;
        [SerializeField] float rotationSpeed = 10;

        [Header("Stamina Costs")] 
        [SerializeField, Min(0.1f)] float sprintCost;
        [SerializeField, Min(1)] int rollCost;
        [SerializeField, Min(1)] int backStepCost;
            
        Transform _camManagerTransform;
        Transform _camObjTransform;
        Vector3 _moveDirection;
        Vector3 _targetRotation;
        Vector2 _inputMovement;

        Vector3 _dodgeDirection;

        protected override void Awake() {
            base.Awake();
            _playerManager = GetComponent<PlayerManager>();
        }
        
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
            if (_playerManager.movementLocked) return;
            _inputMovement = _playerManager.inputManager.MovementInput;
            _moveDirection = _camManagerTransform.forward * _inputMovement.y + 
                             _camManagerTransform.right * _inputMovement.x;
            _moveDirection.y = 0;
            _moveDirection.Normalize();
            _playerManager.controller.Move(GetGroundSpeed() * Time.deltaTime * _moveDirection);
            _playerManager.animManager.UpdateMovementParameters(0, _playerManager.inputManager.MoveAmount);
        }

        float GetGroundSpeed()
        {
            if (_playerManager.isSprinting) return sprintSpeed;
            switch (_playerManager.inputManager.MoveAmount) {
                case 0.5f:
                    return walkingSpeed;
                case 1:
                    return runningSpeed;
                default: return 0;
            }
        }

        void HandleRotation() {
            if (_playerManager.rotationLocked) return;
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
            if (_playerManager.inputManager.MoveAmount > 0) {
                CheckRotationRelativeToCam();
                Quaternion newRotation = Quaternion.LookRotation(_targetRotation);
                transform.rotation = newRotation;
                _playerManager.animManager.PlayTargetAnimation("Dodge_F", false);
                _playerManager.statManager.UseStamina(rollCost);
                return;
            }
            _playerManager.animManager.PlayTargetAnimation("Dodge_B", false);
            _playerManager.statManager.UseStamina(backStepCost);
        }

        public void HandleSprint() {
            if (!CanPerformStaminaAction()) {
                _playerManager.isSprinting = false;
                return;
            }
            _playerManager.isSprinting = true;
            _playerManager.statManager.UseStamina(sprintCost * Time.deltaTime);
        }

        bool CanPerformStaminaAction() {
            if (_playerManager.isPerformingAction) return false;
            return _playerManager.statManager.CurrentStamina > 0;
        }
    }
}
