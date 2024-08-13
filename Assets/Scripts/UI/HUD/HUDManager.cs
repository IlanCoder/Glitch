using Characters.Player;
using UI.HUD.UIObjects;
using UnityEngine;

namespace UI.HUD {
    public class HUDManager : UISliderManager {
        PlayerManager _player;
        
        [Header("Player Stat Bars")]
        [SerializeField] UIStatBar staminaBar;

        [Header("Weapon Slots")]
        [SerializeField] UIWeaponSlot[] weaponSlots = new UIWeaponSlot[3];

        protected override void Awake() {
            _player = manager.GetComponent<PlayerManager>();
            base.Awake();
            SetWeaponSlotsListeners();
        }

        protected override void SetStatBarsListeners() {
            base.SetStatBarsListeners();
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
