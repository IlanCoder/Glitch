using System;
using SaveSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Characters.Player {
    [RequireComponent(typeof(PlayerMovementManager)),
     RequireComponent(typeof(PlayerInputManager)),
     RequireComponent(typeof(PlayerAnimManager)),
     RequireComponent(typeof(PlayerStatsManager)),
     RequireComponent(typeof(PlayerEffectsManager)),
     RequireComponent(typeof(PlayerInventoryManager)),
     RequireComponent(typeof(PlayerEquipmentManager)),
     RequireComponent(typeof(PlayerCombatManager)),
     RequireComponent(typeof(PlayerVFxManager)),
     RequireComponent(typeof(PlayerSFxManager))]
    public class PlayerManager : CharacterManager {
        [SerializeField]PlayerCamera playerCamera;
        [HideInInspector]public PlayerMovementManager movementManager;
        [HideInInspector]public PlayerInputManager inputManager;
        [HideInInspector]public PlayerAnimManager animManager;
        [HideInInspector]public PlayerStatsManager statsManager;
        [HideInInspector]public PlayerEffectsManager effectsManager;
        [HideInInspector]public PlayerInventoryManager inventoryManager;
        [HideInInspector]public PlayerEquipmentManager equipmentManager;
        [HideInInspector]public PlayerCombatManager combatManager;
        [HideInInspector]public PlayerVFxManager vFxManager;
        [HideInInspector]public PlayerSFxManager sFxManager;
        
        public override CharacterStatsManager StatsManager => statsManager;
        public override CharacterAnimManager AnimManager => animManager;
        public override CharacterEffectsManager EffectsManager => effectsManager;
        public override CharacterCombatManager CombatManager => combatManager;
        public override CharacterVFxManager VFxManager => vFxManager;
        public override CharacterSFxManager SFxManager => sFxManager;

        [HideInInspector] public UnityEvent onPlayerDeath;

        protected override void Awake() {
            base.Awake();
            animManager = GetComponent<PlayerAnimManager>();
            inputManager = GetComponent<PlayerInputManager>();
            movementManager = GetComponent<PlayerMovementManager>();
            statsManager = GetComponent<PlayerStatsManager>();
            effectsManager = GetComponent<PlayerEffectsManager>();
            inventoryManager = GetComponent<PlayerInventoryManager>();
            equipmentManager = GetComponent<PlayerEquipmentManager>();
            combatManager = GetComponent<PlayerCombatManager>();
            vFxManager = GetComponent<PlayerVFxManager>();
            sFxManager = GetComponent<PlayerSFxManager>();
            movementManager.playerCam = playerCamera;
            playerCamera.player = this;
            playerCamera.inputManager = inputManager;
        }
        
        protected override void Update() {
            base.Update();
            movementManager.HandleMovement();
            HandleLockOn();
        }

        protected override void LateUpdate() {
            base.LateUpdate();
            playerCamera.HandleCamera();
        }
        
        #region Save & Load
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
        #endregion

        [ContextMenu("Kill")]
        public override void HandleDeathEvent() {
            base.HandleDeathEvent();
            onPlayerDeath?.Invoke();
        }
        
        [ContextMenu("Revive")]
        public override void ReviveCharacter() {
            base.ReviveCharacter();
            statsManager.RevivePlayer();
            animManager.PlayReviveAnimation();
        }

        #region Lock On
        public override void HandleLockOn() {
            if (!isLockedOn) return;
            if (!combatManager.LockOnTarget.isDead) return;
            //Find new target or Unlock
        }

        public void LockOn() {
            playerCamera.FindLockOnTargets();
        }

        public void DisableLockOn() {
            
        }
  #endregion
        
    }
}
