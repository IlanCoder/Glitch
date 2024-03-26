using System;
using Items.Weapons;
using UnityEngine;
using Weapons;


namespace Characters.Player {
    
    public class PlayerEquipmentManager : MonoBehaviour {
        const int WeaponSlotsCount = 3;
        
        PlayerManager _manager;
        
        [Header("Equipment Locations")]
        [SerializeField] Transform rightHandWeaponLocation;
        [SerializeField] Transform leftHandWeaponLocation;

        int _activeWeaponIndex;
        int _nextWeaponIndex;
        BasicWeapon[] _equippedWeapons = new BasicWeapon[WeaponSlotsCount];
        GameObject[] _rightHandWeapons = new GameObject[WeaponSlotsCount];
        GameObject[] _leftHandWeapons = new GameObject[WeaponSlotsCount];

        void Awake() {
            _manager = GetComponent<PlayerManager>();
        }

        public void EquipWeapon(int index, BasicWeapon weapon, GameObject rightWeapon) {
            UnequipWeapon(index);
            _equippedWeapons[index] = weapon;
            _rightHandWeapons[index] = rightWeapon;
            rightWeapon.transform.SetParent(rightHandWeaponLocation, false);
            rightWeapon.GetComponent<WeaponManager>().SetWeaponDamage(weapon);
            if (weapon.DualWield) return;
            if (_activeWeaponIndex != index) return;
            ReactivateWeapon();
        }
        
        public void EquipWeapon(int index, BasicWeapon weapon, GameObject rightWeapon, GameObject leftWeapon) {
            EquipWeapon(index, weapon, rightWeapon);
            _leftHandWeapons[index] = leftWeapon;
            leftWeapon.transform.SetParent(leftHandWeaponLocation, false);
            leftWeapon.GetComponent<WeaponManager>().SetWeaponDamage(weapon);
            if (_activeWeaponIndex != index) return;
            ReactivateWeapon();
        }

        void UnequipWeapon(int index) {
            if (_equippedWeapons[index] == null) return;
            _manager.inventoryManager.UnequipWeapon(_equippedWeapons[index]);
            _equippedWeapons[index] = null;
        }

        public void ChangeActiveWeapon() {
            if (!TryGetNextEquippedWeapon(out int index)) return;
            _manager.animManager.PlayEquipAnimation();
            _nextWeaponIndex = index;
        }

        bool TryGetNextEquippedWeapon(out int index) {
            index = _activeWeaponIndex;
            for (int i = 0; i < _equippedWeapons.Length-1; i++) {
                index++;
                if (index >= _equippedWeapons.Length) index = 0;
                if (_equippedWeapons[index] == null) continue;
                return true;
            }
            return false;
        }

        void ReactivateWeapon() {
            _manager.animManager.PlayEquipAnimation();
        }

        public void SwitchWeapons() {
            DeactivateCurrentWeapon();
            _activeWeaponIndex = _nextWeaponIndex;
            ActivateCurrentWeapon();
        }
        
        void DeactivateCurrentWeapon() {
            if(_equippedWeapons[_activeWeaponIndex] == null) return;
            if (_equippedWeapons[_activeWeaponIndex].DualWield) {
                _leftHandWeapons[_activeWeaponIndex].SetActive(false);
            }
            _rightHandWeapons[_activeWeaponIndex].SetActive(false);
        }

        void ActivateCurrentWeapon() {
            _rightHandWeapons[_activeWeaponIndex].SetActive(true);
            if (_equippedWeapons[_activeWeaponIndex].DualWield) {
                _leftHandWeapons[_activeWeaponIndex].SetActive(true);
            }
        }
    }
}
