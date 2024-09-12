using Characters.Player;
using UI.HUD.UIObjects;
using UnityEngine;

namespace UI.HUD {
    public class HUDManager : MonoBehaviour {
        [SerializeField] PlayerManager player;
        
        [Header("Stat Bars")]
        [SerializeField] UIStatBar healthBar;
        [SerializeField] UIStatBar energyBar;
        [SerializeField] UIStatBar staminaBar;

        [Header("Weapon Slots")]
        [SerializeField] UIWeaponSlot[] weaponSlots = new UIWeaponSlot[3];

        void Start() {
            SetStatBarsListeners();
            SetWeaponSlotsListeners();
            SetStatBarsValues();
        }

        void SetStatBarsValues() {
            healthBar.SetMaxStat(player.statsController.PlayerStats.MaxHp);
            healthBar.SetStat(player.statsController.CurrentHp, false);

            energyBar.SetMaxStat(player.statsController.PlayerStats.MaxEnergy);
            energyBar.SetStat(player.statsController.CurrentEnergy, false);
            
            staminaBar.SetMaxStat(player.statsController.PlayerStats.MaxStamina);
            staminaBar.SetStat(player.statsController.CurrentStamina, false);
        }

        void SetStatBarsListeners() {
            player.StatsController.onMaxHpChange.AddListener(SetNewMaxHpValue);
            player.StatsController.onHpChange.AddListener(SetNewHpValue);
            
            player.StatsController.onMaxEnergyChange.AddListener(SetNewMaxEnergyValue);
            player.StatsController.onEnergyChange.AddListener(SetNewEnergyValue);
            
            player.statsController.onMaxStaminaChange.AddListener(SetNewMaxStaminaValue);
            player.statsController.onStaminaChange.AddListener(SetNewStaminaValue);
        }

        void SetWeaponSlotsListeners() {
            player.equipmentManager.onEquipWeapon.AddListener(SetWeaponSlotSprite);
            player.equipmentManager.onUnequipWeapon.AddListener(RemoveWeaponSlotSprite);
            player.equipmentManager.onActiveWeaponSwitch.AddListener(SwitchActiveWeapon);
        }

        #region Stat Bars
        void SetNewStaminaValue(float newValue) {
            staminaBar.SetStat(newValue);
        }

        void SetNewMaxStaminaValue(int newMax) {
            staminaBar.SetMaxStat(newMax);
        }
        
        void SetNewHpValue(int newValue) {
            healthBar.SetStat(newValue);
        }

        void SetNewMaxHpValue(int newMax) {
            healthBar.SetMaxStat(newMax);
        }

        void SetNewEnergyValue(float newValue) {
            energyBar.SetStat(newValue);
        }
        
        void SetNewMaxEnergyValue(int newMax) {
            energyBar.SetMaxStat(newMax);
        }
#endregion

        #region Weapon Slots
        void SetWeaponSlotSprite(int index, Sprite sprite) {
            weaponSlots[index].SetSprite(sprite);
            weaponSlots[index].gameObject.SetActive(true);
        }
        
        void RemoveWeaponSlotSprite(int index) {
            weaponSlots[index].SetSprite(null);
            weaponSlots[index].gameObject.SetActive(false);
        }

        void SwitchActiveWeapon(int currentIndex, int newIndex) {
            if(weaponSlots[newIndex].IsBig) return;
            weaponSlots[currentIndex].GoSmall();
            weaponSlots[newIndex].GoBig();
        }
#endregion
    }
}
