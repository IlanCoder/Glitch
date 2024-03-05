using System;
using Characters.Player.PlayerUI;
using SaveSystem;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Characters.Player {
    [RequireComponent(typeof(PlayerMovementManager)),
     RequireComponent(typeof(PlayerInputManager)),
     RequireComponent(typeof(PlayerAnimManager)),
     RequireComponent(typeof(PlayerStatManager))]
    public class PlayerManager : CharacterManager {
        [SerializeField] PlayerCamera playerCamera;
        [HideInInspector]public PlayerMovementManager movementManager;
        [HideInInspector]public PlayerInputManager inputManager;
        [HideInInspector]public PlayerAnimManager animManager;
        [HideInInspector]public PlayerStatManager statManager;
        
        protected override void Awake() {
            base.Awake();
            animManager = GetComponent<PlayerAnimManager>();
            inputManager = GetComponent<PlayerInputManager>();
            movementManager = GetComponent<PlayerMovementManager>();
            statManager = GetComponent<PlayerStatManager>();
            movementManager.playerCam = playerCamera;
            playerCamera.player = this;
            playerCamera.inputManager = inputManager;
        }
        
        protected override void Update() {
            base.Update();
            movementManager.HandleMovement();
        }

        protected override void LateUpdate() {
            base.LateUpdate();
            playerCamera.HandleCamera();
        }

        public void SavePlayerData(ref PlayerSaveData saveData) {
            saveData.PlayerName = statManager.characterName;
            Vector3 position = transform.position;
            saveData.PlayerXPos = position.x;
            saveData.PlayerYPos = position.y;
            saveData.PlayerZPos = position.y;
        }

        public void LoadPlayerData(ref PlayerSaveData saveData) {
            statManager.characterName = saveData.PlayerName;
            transform.position = new Vector3(saveData.PlayerXPos, saveData.PlayerYPos, saveData.PlayerZPos);
        }
    }
}
