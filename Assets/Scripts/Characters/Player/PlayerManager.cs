using UnityEngine;

namespace Characters.Player {
    [RequireComponent(typeof(PlayerMovementManager))]
    public class PlayerManager : CharacterManager {
        PlayerMovementManager _movementManager;
        [SerializeField] PlayerCamera playerCamera;
        protected override void Awake() {
            base.Awake();
            _movementManager = GetComponent<PlayerMovementManager>();
            _movementManager.playerCam = playerCamera;
            playerCamera.player = this;
            playerCamera.inputManager = GetComponent<PlayerInputManager>();
        }

        protected override void Update() {
            base.Update();
            _movementManager.HandleMovement();
        }

        protected override void LateUpdate() {
            base.LateUpdate();
            playerCamera.HandleCamera();
        }
    }
}
