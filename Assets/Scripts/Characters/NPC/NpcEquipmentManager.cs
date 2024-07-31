using System;
using Items.Weapons;
using Items.Weapons.MeleeWeapons;
using UnityEngine;
using Weapons;

namespace Characters.NPC {
    public class NpcEquipmentManager : CharacterEquipmentManager {
        NpcManager _npcManager;
        
        [Header("Equipment Settings")]
        [SerializeField] BasicWeapon equippedWeapon;
        [SerializeField] bool startEquipped;
        
        [Header("Unequipped Locations")]
        [SerializeField] Transform rightHandUnequippedLocation;
        [SerializeField] Transform leftHandUnequippedLocation;
        
        GameObject _rightHandWeapon;
        GameObject _leftHandWeapon;
        
        protected override void Awake() {
            base.Awake();
            _npcManager = GetComponent<NpcManager>();
        }

        protected void Start() {
            equippedWeapon = Instantiate(equippedWeapon, transform);
            
            equippedWeapon.InitializeWeapon();
            InstantiateUnequippedWeapon();
            if(startEquipped) EquipWeapon();
        }

        void InstantiateUnequippedWeapon() {
            if (!equippedWeapon) return;
            _rightHandWeapon = Instantiate(equippedWeapon.WeaponPrefab, rightHandUnequippedLocation);
            _rightHandWeapon.GetComponent<WeaponManager>().SetWeapon(equippedWeapon, _npcManager);
            _rightHandWeapon.SetActive(true);
            
            if (!equippedWeapon.DualWield) return;
            _leftHandWeapon = Instantiate(equippedWeapon.OffhandPrefab, leftHandUnequippedLocation);
            _leftHandWeapon.GetComponent<WeaponManager>().SetWeapon(equippedWeapon, _npcManager);
            _leftHandWeapon.SetActive(true);
        }

        void EquipWeapon() {
            if (!equippedWeapon) return;
            if (!_rightHandWeapon) return;
            _rightHandWeapon.transform.SetParent(rightHandWeaponLocation, false);
            if (!equippedWeapon.DualWield) return;
            _leftHandWeapon.transform.SetParent(leftHandWeaponLocation, false);
        }

        #region Animation Events
        public void GrabWeapon() {
            if (startEquipped) return;
            EquipWeapon();
        }
        
        public void EnableWeaponColliders(int hand = 0) {
            if (!equippedWeapon) return;
            if (!equippedWeapon.DualWield) hand = 1;
            switch (hand) {
                case 0:
                    _rightHandWeapon.GetComponent<WeaponManager>().EnableDamageCollider();
                    _leftHandWeapon.GetComponent<WeaponManager>().EnableDamageCollider();
                    break;
                case 1:
                    _rightHandWeapon.GetComponent<WeaponManager>().EnableDamageCollider();
                    break;
                case >=2:
                    _leftHandWeapon.GetComponent<WeaponManager>().EnableDamageCollider();
                    break;
            }
        }

        public void DisableWeaponColliders(int hand = 0) {
            if (!equippedWeapon) return;
            if (!equippedWeapon.DualWield) hand = 1;
            switch (hand) {
                case 0:
                    _rightHandWeapon.GetComponent<WeaponManager>().DisableDamageCollider();
                    _leftHandWeapon.GetComponent<WeaponManager>().DisableDamageCollider();
                    break;
                case 1:
                    _rightHandWeapon.GetComponent<WeaponManager>().DisableDamageCollider();
                    break;
                case >=2:
                    _leftHandWeapon.GetComponent<WeaponManager>().DisableDamageCollider();
                    break;
            }
        }
        #endregion
    }
}