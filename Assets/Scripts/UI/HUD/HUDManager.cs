using System;
using Characters.Player;
using UnityEngine;

namespace UI.HUD {
    public class HUDManager : MonoBehaviour {
        [SerializeField] PlayerManager player;
        
        [Header("HUD Bars")]
        [SerializeField] UIStatBar healthBar;
        [SerializeField] UIStatBar staminaBar;
        [SerializeField] UIStatBar energyBar;

        [Header("Weapon Slots")]
        [SerializeField] UIWeaponSlot[] weaponSlots = new UIWeaponSlot[3];

        void Awake() {
            SetStatBarsListeners();
            SetWeaponSlotsListeners();
        }

        void SetStatBarsListeners() {
            player.statsManager.onMaxStaminaChange.AddListener(SetNewMaxStaminaValue);
            player.statsManager.onStaminaChange.AddListener(SetNewStaminaValue);
            player.statsManager.onMaxHpChange.AddListener(SetNewMaxHpValue);
            player.statsManager.onHpChange.AddListener(SetNewHpValue);
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
