using System;
using SaveSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Player {
    [RequireComponent(typeof(PlayerMovementManager)),
     RequireComponent(typeof(PlayerInputManager)),
     RequireComponent(typeof(PlayerAnimManager)),
     RequireComponent(typeof(PlayerStatsManager)),
     RequireComponent(typeof(PlayerEffectsManager))]
    public class PlayerManager : CharacterManager {
        [SerializeField]PlayerCamera playerCamera;
        [HideInInspector]public PlayerMovementManager movementManager;
        [HideInInspector]public PlayerInputManager inputManager;
        [HideInInspector]public PlayerAnimManager animManager;
        [FormerlySerializedAs("statManager")] [HideInInspector]public PlayerStatsManager statsManager;
        [HideInInspector]public PlayerEffectsManager effectsManager;
        public override CharacterStatsManager StatsManager => statsManager;

        protected override void Awake() {
            base.Awake();
            animManager = GetComponent<PlayerAnimManager>();
            inputManager = GetComponent<PlayerInputManager>();
            movementManager = GetComponent<PlayerMovementManager>();
            statsManager = GetComponent<PlayerStatsManager>();
            effectsManager = GetComponent<PlayerEffectsManager>();
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
            saveData.PlayerName = statsManager.characterName;
            Vector3 position = transform.position;
            saveData.PlayerXPos = position.x;
            saveData.PlayerYPos = position.y;
            saveData.PlayerZPos = position.z;
            SavePlayerAttributes(ref saveData);
            saveData.CurrentHp = statsManager.CurrentHp;
            saveData.CurrentStamina = statsManager.CurrentStamina;
        }

        void SavePlayerAttributes(ref PlayerSaveData saveData) {
            saveData.Vitality = statsManager.Vitality;
            saveData.Endurance = statsManager.Endurance;
            saveData.Dexterity = statsManager.Dexterity;
            saveData.Strength = statsManager.Strength;
            saveData.Cyber = statsManager.Cyber;
            saveData.Control = statsManager.Control;
        }

        public void LoadPlayerData(ref PlayerSaveData saveData) {
            statsManager.characterName = saveData.PlayerName;
            transform.position = new Vector3(saveData.PlayerXPos, saveData.PlayerYPos, saveData.PlayerZPos);
            statsManager.LoadCharacterAttributes(saveData.Vitality, saveData.Endurance, saveData.Dexterity,
                saveData.Strength, saveData.Cyber, saveData.Control);
            statsManager.LoadCurrentStats(saveData.CurrentHp, saveData.CurrentStamina);
        }
    }
}
