using Characters.Player;
using UI.HUD.UIObjects;
using UnityEngine;

namespace UI.HUD {
    public class HUDManager : MonoBehaviour {
        PlayerManager _player;
        
        [Header("Stat Bars")]
        [SerializeField] UIStatBar healthBar;
        [SerializeField] UIStatBar energyBar;
        [SerializeField] UIStatBar staminaBar;

        [Header("Weapon Slots")]
        [SerializeField] UIWeaponSlot[] weaponSlots = new UIWeaponSlot[3];

        void Awake() {
            _player = GetComponent<PlayerManager>();
            SetStatBarsListeners();
            SetWeaponSlotsListeners();
        }

        void SetStatBarsListeners() {
            _player.StatsController.onMaxHpChange.AddListener(SetNewMaxHpValue);
            _player.StatsController.onHpChange.AddListener(SetNewHpValue);
            
            _player.StatsController.onMaxEnergyChange.AddListener(SetNewMaxEnergyValue);
            _player.StatsController.onEnergyChange.AddListener(SetNewEnergyValue);
            
            _player.statsController.onMaxStaminaChange.AddListener(SetNewMaxStaminaValue);
            _player.statsController.onStaminaChange.AddListener(SetNewStaminaValue);
        }

        void SetWeaponSlotsListeners() {
            _player.equipmentManager.onEquipWeapon.AddListener(SetWeaponSlotSprite);
            _player.equipmentManager.onUnequipWeapon.AddListener(RemoveWeaponSlotSprite);
            _player.equipmentManager.onActiveWeaponSwitch.AddListener(SwitchActiveWeapon);
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
