using System;
using System.Threading.Tasks;
using SaveSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Characters.Player {
    [RequireComponent(typeof(PlayerMovementController)),
     RequireComponent(typeof(PlayerInputController)),
     RequireComponent(typeof(PlayerAnimController)),
     RequireComponent(typeof(PlayerAnimOverrider)),
     RequireComponent(typeof(PlayerStatsController)),
     RequireComponent(typeof(PlayerInventoryManager)),
     RequireComponent(typeof(PlayerEquipmentManager)),
     RequireComponent(typeof(PlayerCombatController)),]
    public class PlayerManager : CharacterManager {
        [SerializeField]PlayerCamera playerCamera;
        [HideInInspector]public PlayerMovementController movementController;
        [HideInInspector]public PlayerInputController inputController;
        [HideInInspector]public PlayerAnimController animController;
        [HideInInspector]public PlayerAnimOverrider animOverrider;
        [HideInInspector]public PlayerStatsController statsController;
        [HideInInspector]public PlayerInventoryManager inventoryManager;
        [HideInInspector]public PlayerEquipmentManager equipmentManager;
        [HideInInspector]public PlayerCombatController combatController;

        public override CharacterMovementController MovementController => movementController;
        public override CharacterStatsController StatsController => statsController;
        public override CharacterAnimController AnimController => animController;
        public override CharacterCombatController CombatController => combatController;

        [HideInInspector] public UnityEvent onPlayerDeath;

        protected override void Awake() {
            base.Awake();
            animController = GetComponent<PlayerAnimController>();
            animOverrider = GetComponent<PlayerAnimOverrider>();
            inputController = GetComponent<PlayerInputController>();
            movementController = GetComponent<PlayerMovementController>();
            statsController = GetComponent<PlayerStatsController>();
            inventoryManager = GetComponent<PlayerInventoryManager>();
            equipmentManager = GetComponent<PlayerEquipmentManager>();
            combatController = GetComponent<PlayerCombatController>();
            movementController.playerCam = playerCamera;
            playerCamera.player = this;
            playerCamera.inputController = inputController;
        }
        
        protected override void Update() {
            base.Update();
            movementController.HandleMovement();
            HandleLockOn();
        }

        protected override void LateUpdate() {
            base.LateUpdate();
            playerCamera.HandleCamera();
        }
        
        #region Save & Load
        public void SavePlayerData(ref PlayerSaveData saveData) {
            saveData.PlayerName = statsController.characterName;
            Vector3 position = transform.position;
            saveData.PlayerXPos = position.x;
            saveData.PlayerYPos = position.y;
            saveData.PlayerZPos = position.z;
            SavePlayerAttributes(ref saveData);
            saveData.CurrentHp = statsController.CurrentHp;
            saveData.CurrentStamina = statsController.CurrentStamina;
            saveData.CurrentEnergy = statsController.CurrentEnergy;
        }

        void SavePlayerAttributes(ref PlayerSaveData saveData) {
            saveData.Vitality = statsController.Vitality;
            saveData.Endurance = statsController.Endurance;
            saveData.Dexterity = statsController.Dexterity;
            saveData.Strength = statsController.Strength;
            saveData.Cyber = statsController.Cyber;
            saveData.Control = statsController.Control;
        }

        public void LoadPlayerData(ref PlayerSaveData saveData) {
            statsController.characterName = saveData.PlayerName;
            transform.position = new Vector3(saveData.PlayerXPos, saveData.PlayerYPos, saveData.PlayerZPos);
            statsController.LoadCharacterAttributes(saveData.Vitality, saveData.Endurance, saveData.Dexterity,
                saveData.Strength, saveData.Cyber, saveData.Control);
            statsController.LoadCurrentStats(saveData.CurrentHp, saveData.CurrentStamina, saveData.CurrentEnergy);
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
            statsController.RevivePlayer();
            animController.PlayReviveAnimation();
        }

        #region Lock On
        public void LockOn() {
            if (!playerCamera.FindClosestLockOnTarget(out CharacterManager target)) return;
            combatController.ChangeTarget(target);
            isLockedOn = true;
        }

        public void DisableLockOn() {
            isLockedOn = false;
            playerCamera.UnlockCamera();
            combatController.ChangeTarget(null);
        }

        public void TryNewLockOn() {
            if (!playerCamera.FindClosestLockOnTarget(out CharacterManager target)) {
                DisableLockOn();
            }
            combatController.ChangeTarget(target);
        }
        
        public void SwitchLockOn(float value) {
            CharacterManager target;
            if (value > 0) {
                if (!playerCamera.FindClosestRightLockOnTarget(out target)) return;
                combatController.ChangeTarget(target);
                return;
            }
            if (!playerCamera.FindClosestLeftLockOnTarget(out target)) return;
            combatController.ChangeTarget(target);
        }
  #endregion
    }
}
