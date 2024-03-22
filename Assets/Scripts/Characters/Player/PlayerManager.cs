using SaveSystem;
using UnityEngine;

namespace Characters.Player {
    [RequireComponent(typeof(PlayerMovementManager)),
     RequireComponent(typeof(PlayerInputManager)),
     RequireComponent(typeof(PlayerAnimManager)),
     RequireComponent(typeof(PlayerStatManager)),
     RequireComponent(typeof(PlayerEffectsManager))]
    public class PlayerManager : CharacterManager {
        [SerializeField]PlayerCamera playerCamera;
        [HideInInspector]public PlayerMovementManager movementManager;
        [HideInInspector]public PlayerInputManager inputManager;
        [HideInInspector]public PlayerAnimManager animManager;
        [HideInInspector]public PlayerStatManager statManager;
        [HideInInspector]public PlayerEffectsManager effectsManager;

        protected override void Awake() {
            base.Awake();
            animManager = GetComponent<PlayerAnimManager>();
            inputManager = GetComponent<PlayerInputManager>();
            movementManager = GetComponent<PlayerMovementManager>();
            statManager = GetComponent<PlayerStatManager>();
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
            saveData.PlayerName = statManager.characterName;
            Vector3 position = transform.position;
            saveData.PlayerXPos = position.x;
            saveData.PlayerYPos = position.y;
            saveData.PlayerZPos = position.z;
            SavePlayerAttributes(ref saveData);
            saveData.CurrentHp = statManager.CurrentHp;
            saveData.CurrentStamina = statManager.CurrentStamina;
        }

        void SavePlayerAttributes(ref PlayerSaveData saveData) {
            saveData.Vitality = statManager.Vitality;
            saveData.Endurance = statManager.Endurance;
            saveData.Dexterity = statManager.Dexterity;
            saveData.Strength = statManager.Strength;
            saveData.Cyber = statManager.Cyber;
            saveData.Control = statManager.Control;
        }

        public void LoadPlayerData(ref PlayerSaveData saveData) {
            statManager.characterName = saveData.PlayerName;
            transform.position = new Vector3(saveData.PlayerXPos, saveData.PlayerYPos, saveData.PlayerZPos);
            statManager.LoadCharacterAttributes(saveData.Vitality, saveData.Endurance, saveData.Dexterity,
                saveData.Strength, saveData.Cyber, saveData.Control);
            statManager.LoadCurrentStats(saveData.CurrentHp, saveData.CurrentStamina);
        }
    }
}
