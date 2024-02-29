using System.Diagnostics;
using UnityEngine;

namespace Characters.Player {
    [RequireComponent(typeof(PlayerManager)), 
     RequireComponent(typeof(PlayerInputManager))]
    public class PlayerMovementManager : CharacterMovementScript {
        [HideInInspector]public PlayerCamera playerCam;

        [Header("Speeds")]
        [SerializeField] float walkingSpeed = 2;
        [SerializeField] float runningSpeed = 4;
        [SerializeField] float rotationSpeed = 10;
        
        PlayerManager _playerManager;
        PlayerInputManager _inputManager;

        Transform _camTransform;
        Vector3 _moveDirection;
        Vector3 _targetRotation;
        Vector2 _inputMovement;


        protected override void Awake() {
            base.Awake();
            _inputManager = GetComponent<PlayerInputManager>();
            _playerManager = GetComponent<PlayerManager>();
        }
        
        public void HandleMovement() {
            HandleGroundMovement();
            HandleRotation();
        }

        void HandleGroundMovement() {
            _camTransform = playerCam.transform;
            _inputMovement = _inputManager.MovementInput;
            _moveDirection = _camTransform.forward * _inputMovement.y + _camTransform.right * _inputMovement.x;
            _moveDirection.y = 0;
            _moveDirection.Normalize();

            switch (_inputManager.MoveAmount) {
                case 0.5f:
                    _playerManager.controller.Move(walkingSpeed * Time.deltaTime * _moveDirection);
                    break;
                case 1:
                    _playerManager.controller.Move(runningSpeed * Time.deltaTime * _moveDirection);
                    break;
            }
        }

        void HandleRotation() {
            Transform tempCamTransform = playerCam.cam.transform;
            _targetRotation = tempCamTransform.forward * _inputMovement.y + tempCamTransform.right * _inputMovement.x;
            _targetRotation.y = 0;
            _targetRotation.Normalize();

            if (_targetRotation == Vector3.zero) return;
            Quaternion newRotation = Quaternion.LookRotation(_targetRotation);
            Quaternion targetRotation =
                Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }
    }
}
