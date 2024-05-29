using Enums;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using WorldManager;

namespace Characters.Player {
    public class PlayerInputController : MonoBehaviour {
        [SerializeField] WorldSaveManager worldSaveManager;
        PlayerControls _playerControls;
        PlayerManager _playerManager;
        
        #region Player Movement Vars
        public Vector2 MovementInput { get; private set; }
        public float MoveAmount{ get; private set; }
        bool _sprint = false;
#endregion

        #region Camera Vars
        public Vector2 CameraInput { get; private set; }
#endregion

        void Awake() {
            SceneManager.activeSceneChanged += OnSceneChanged;
            _playerManager = GetComponent<PlayerManager>();
            enabled = false;
        }
        
        void OnEnable() {
            if (_playerControls == null) {
                _playerControls = new PlayerControls();
                _playerControls.PlayerMovement.Movement.performed += i => 
                    MovementInput = i.ReadValue<Vector2>();
                _playerControls.PlayerMovement.Jump.performed += i => HandleJump();
                _playerControls.PlayerMovement.Dodge.performed += i => HandleDodge();
                _playerControls.PlayerMovement.Sprint.performed += i => _sprint = true;
                _playerControls.PlayerMovement.Sprint.canceled += i => _sprint = false;

                _playerControls.PlayerCamera.Movement.performed += i => 
                    CameraInput = i.ReadValue<Vector2>();
                _playerControls.PlayerCamera.LockOn.performed += i => HandleLockOn();
                _playerControls.PlayerCamera.ChangeLockOnTarget.performed += i => 
                    HandleLockOnChange(i.ReadValue<float>());

                _playerControls.PlayerActions.ChangeWeapon.performed += i => HandleActiveWeaponChange();
                _playerControls.PlayerActions.LightAttack.performed += i => HandleLightAttack();
                _playerControls.PlayerActions.HeavyAttack.performed += i => HandleHeavyAttack();
            }
            _playerControls.Enable();
        }

        void OnDisable() {
            _playerControls?.Disable();
        }

        void OnDestroy() {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

        void Update() {
            HandleSprint();
            HandleMovement();
        }

        void OnSceneChanged(Scene oldScene, Scene newScene) {
            enabled = newScene.buildIndex == worldSaveManager.GetSceneIndex();
        }

        #region Locomotion
        void HandleMovement() {
            MoveAmount = Mathf.Clamp01(Mathf.Abs(MovementInput.x) + Mathf.Abs(MovementInput.y));
            if (_playerManager.isSprinting) {
                MoveAmount = 2;
                return;
            }
            switch (MoveAmount) {
                case 0: break;
                case <= 0.5f:
                    MoveAmount = 0.5f;
                    break;
                default: MoveAmount = 1;
                    break;
            }
        }

        void HandleJump() {
            _playerManager.movementController.AttemptToJump();
        }
        
        void HandleDodge() {
            _playerManager.movementController.AttemptToDodge();
        }

        void HandleSprint() {
            if (!_sprint) {
                if(!_playerManager.isSprinting) return;
                _playerManager.isSprinting = false;
                return;
            }
            _playerManager.movementController.HandleSprint();
        }
  #endregion
        
        void HandleActiveWeaponChange() {
            _playerManager.equipmentManager.ChangeActiveWeapon();
        }

        void HandleLightAttack() {
            _playerManager.combatController.PerformNormalAttack(AttackType.Light);
        }

        void HandleHeavyAttack() {
            _playerManager.combatController.PerformNormalAttack(AttackType.Heavy);
        }

        #region LockOn
        void HandleLockOn() {
            if (_playerManager.isLockedOn) {
                _playerManager.DisableLockOn();
                return;
            }
            _playerManager.LockOn();
        }

        void HandleLockOnChange(float value) {
            if (!_playerManager.isLockedOn) return;
            if (value == 0) return;
            _playerManager.SwitchLockOn(value);
        }
#endregion
    }
}