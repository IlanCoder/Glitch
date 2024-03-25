using System;
using Items.Weapons;
using UnityEngine;

namespace Characters.Player {
    public class PlayerEquipmentManager : MonoBehaviour {
        PlayerManager _manager;
        
        [Header("Equipment Locations")]
        [SerializeField] Transform rightHandWeaponLocation;
        [SerializeField] Transform leftHandWeaponLocation;
        
        [Header("Equipment")]
        [SerializeField] BasicWeapon equippedWeapon;

        void Awake() {
            _manager = GetComponent<PlayerManager>();
        }

        public void EquipWeapon(BasicWeapon weapon, GameObject right) {
            UnequipWeapon();
            equippedWeapon = weapon;
            right.transform.SetParent(rightHandWeaponLocation, false);
            right.SetActive(true);
        }
        
        public void EquipWeapon(BasicWeapon weapon, GameObject right, GameObject left) {
            EquipWeapon(weapon, right);
            left.transform.SetParent(leftHandWeaponLocation, false);
            left.SetActive(true);
        }

        void UnequipWeapon() {
            if(equippedWeapon==null) return;
            _manager.inventoryManager.UnequipWeapon(equippedWeapon);
            equippedWeapon = null;
        }
    }
}
