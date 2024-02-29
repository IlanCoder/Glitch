using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using WorldManager;

namespace Characters.Player {
    public class PlayerInputManager : MonoBehaviour {

        [SerializeField] WorldSaveManager worldSaveManager;
        PlayerControls _playerControls;
        
        public Vector2 MovementInput { get; private set; }
        public float MoveAmount{ get; private set; }
        
        public Vector2 CameraInput { get; private set; }

        void Awake() {
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += OnSceneChanged;
            enabled = false;
        }
        
        void OnEnable() {
            _playerControls ??= new PlayerControls();
            _playerControls.PlayerMovement.Movement.performed += i => 
                MovementInput = i.ReadValue<Vector2>();
            _playerControls.PlayerCamera.Movement.performed += i => 
                CameraInput = i.ReadValue<Vector2>();
            _playerControls.Enable();
        }

        void OnDisable() {
            _playerControls?.Disable();
        }

        void OnDestroy() {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

        void Update() {
            ClampPlayerMovement();
        }

        void OnSceneChanged(Scene oldScene, Scene newScene) {
            enabled = newScene.buildIndex == worldSaveManager.GetSceneIndex();
        }

        void ClampPlayerMovement() {
            MoveAmount = Mathf.Clamp01(Mathf.Abs(MovementInput.x) + Mathf.Abs(MovementInput.y));
            switch (MoveAmount) {
                case 0: break;
                case <= 0.5f:
                    MoveAmount = 0.5f;
                    break;
                default: MoveAmount = 1;
                    break;
            }
        }
    }
}
