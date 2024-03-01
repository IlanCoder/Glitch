using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using WorldManager;

namespace Characters.Player {
    public class PlayerInputManager : MonoBehaviour {
        [SerializeField] WorldSaveManager worldSaveManager;
        PlayerControls _playerControls;
        PlayerManager _playerManager;
        #region Player Movement Vars
        public Vector2 MovementInput { get; private set; }
        public float MoveAmount{ get; private set; }
#endregion
        #region Camera Vars
        public Vector2 CameraInput { get; private set; }
#endregion

        #region Dodge Vars
        bool _dodge = false;
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
                _playerControls.PlayerMovement.Dodge.performed += i => _dodge = true;
                _playerControls.PlayerCamera.Movement.performed += i => 
                CameraInput = i.ReadValue<Vector2>();
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
            HandleMovement();
            HandleDodge();
        }

        void OnSceneChanged(Scene oldScene, Scene newScene) {
            enabled = newScene.buildIndex == worldSaveManager.GetSceneIndex();
        }

        void HandleMovement() {
            MoveAmount = Mathf.Clamp01(Mathf.Abs(MovementInput.x) + Mathf.Abs(MovementInput.y));
            switch (MoveAmount) {
                case 0: break;
                case <= 0.5f:
                    MoveAmount = 0.5f;
                    break;
                default: MoveAmount = 1;
                    break;
            }
            _playerManager.animManager.UpdateMovementParameters(0, MoveAmount);
        }

        void HandleDodge() {
            if (_dodge) {
                _dodge = false;
                _playerManager.movementManager.AttemptToDodge();
            }
        }
    }
}
