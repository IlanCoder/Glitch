using System;
using Items.Weapons;
using UnityEngine;
using UnityEngine.Events;
using Weapons;


namespace Characters.Player {
    public class PlayerEquipmentManager : CharacterEquipmentManager {
        const int WeaponSlotsCount = 3;
        
        PlayerManager _playerManager;

        int _activeWeaponIndex;
        int _nextWeaponIndex;
        BasicWeapon[] _equippedWeapons = new BasicWeapon[WeaponSlotsCount];
        GameObject[] _rightHandWeapons = new GameObject[WeaponSlotsCount];
        GameObject[] _leftHandWeapons = new GameObject[WeaponSlotsCount];

        [HideInInspector] public UnityEvent<int, Sprite> onEquipWeapon;
        [HideInInspector] public UnityEvent<int> onUnequipWeapon;
        [HideInInspector] public UnityEvent<int, int> onActiveWeaponSwitch;
        
        protected override void Awake() {
            base.Awake();
            _playerManager = GetComponent<PlayerManager>();
        }

        public void EquipWeapon(int index, BasicWeapon weapon, GameObject rightWeapon) {
            UnequipWeapon(index);
            _equippedWeapons[index] = weapon;
            _rightHandWeapons[index] = rightWeapon;
            rightWeapon.transform.SetParent(rightHandWeaponLocation, false);
            rightWeapon.GetComponent<WeaponManager>().SetWeapon(weapon, _playerManager);
            if (weapon.DualWield) return;
            SetWeaponSlot(index, weapon);
        }
        
        public void EquipWeapon(int index, BasicWeapon weapon, GameObject rightWeapon, GameObject leftWeapon) {
            EquipWeapon(index, weapon, rightWeapon);
            _leftHandWeapons[index] = leftWeapon;
            leftWeapon.transform.SetParent(leftHandWeaponLocation, false);
            leftWeapon.GetComponent<WeaponManager>().SetWeapon(weapon, _playerManager);
            SetWeaponSlot(index, weapon);
        }

        void SetWeaponSlot(int index, BasicWeapon weapon) {
            onEquipWeapon?.Invoke(index, weapon.Icon);
            if (_activeWeaponIndex != index) return;
            ActivateWeapon();
        }

        void UnequipWeapon(int index) {
            if (_equippedWeapons[index] == null) return;
            _playerManager.inventoryManager.UnequipWeapon(_equippedWeapons[index]);
            _equippedWeapons[index] = null;
            onUnequipWeapon?.Invoke(index);
        }

        public void ChangeActiveWeapon() {
            if (!TryGetNextEquippedWeapon(out int index)) return;
            _playerManager.animController.PlayEquipAnimation();
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

        void ActivateCurrentWeapon(bool active) {
            if(_equippedWeapons[_activeWeaponIndex] == null) return;
            _rightHandWeapons[_activeWeaponIndex].SetActive(active);
            if (_equippedWeapons[_activeWeaponIndex].DualWield) _leftHandWeapons[_activeWeaponIndex].SetActive(active);
        }

        void SetCombatWeapon() {
            _playerManager.combatController.SetActiveWeapon(_equippedWeapons[_activeWeaponIndex]); 
            _playerManager.combatController.rightHandWeaponManager =
                _rightHandWeapons[_activeWeaponIndex].GetComponent<WeaponManager>();
            if (_equippedWeapons[_activeWeaponIndex].DualWield) _playerManager.combatController.leftHandWeaponManager =
                _leftHandWeapons[_activeWeaponIndex].GetComponent<WeaponManager>();
        }
        
        #region Animation Events
        public void SwitchWeapons() {
            onActiveWeaponSwitch?.Invoke(_activeWeaponIndex, _nextWeaponIndex);
            ActivateCurrentWeapon(false);
            _activeWeaponIndex = _nextWeaponIndex;
            ActivateCurrentWeapon(true);
            SetCombatWeapon();
        }

        public void EnableWeaponColliders(int hand = 0) {
            if(_equippedWeapons[_activeWeaponIndex] == null) return;
            if (!_equippedWeapons[_activeWeaponIndex].DualWield) hand = 1;
            switch (hand) {
                case 0:
                    _rightHandWeapons[_activeWeaponIndex].GetComponent<WeaponManager>().EnableDamageCollider();
                    _leftHandWeapons[_activeWeaponIndex].GetComponent<WeaponManager>().EnableDamageCollider();
                    break;
                case 1:
                    _rightHandWeapons[_activeWeaponIndex].GetComponent<WeaponManager>().EnableDamageCollider();
                    break;
                case >=2:
                    _leftHandWeapons[_activeWeaponIndex].GetComponent<WeaponManager>().EnableDamageCollider();
                    break;
            }
        }

        public void DisableWeaponColliders(int hand = 0) {
            if(_equippedWeapons[_activeWeaponIndex] == null) return;
            if (!_equippedWeapons[_activeWeaponIndex].DualWield) hand = 1;
            switch (hand) {
                case 0:
                    _rightHandWeapons[_activeWeaponIndex].GetComponent<WeaponManager>().DisableDamageCollider();
                    _leftHandWeapons[_activeWeaponIndex].GetComponent<WeaponManager>().DisableDamageCollider();
                    break;
                case 1:
                    _rightHandWeapons[_activeWeaponIndex].GetComponent<WeaponManager>().DisableDamageCollider();
                    break;
                case >=2:
                    _leftHandWeapons[_activeWeaponIndex].GetComponent<WeaponManager>().DisableDamageCollider();
                    break;
            }
        }
        #endregion
    }
}
