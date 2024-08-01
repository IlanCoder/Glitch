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
        
        override public void EnableWeaponAttack(int hand = 0) {
            if(equippedWeapon == null) return;
            if (!equippedWeapon.DualWield) {
                EnableWeaponColliders(_rightHandWeapon.GetComponent<WeaponManager>());
                return;
            }
            EnableWeaponColliders(_rightHandWeapon.GetComponent<WeaponManager>(),
            _leftHandWeapon.GetComponent<WeaponManager>(), hand);
        }   

        override public void DisableWeaponAttack(int hand = 0) {
            if(equippedWeapon == null) return;
            if (!equippedWeapon.DualWield) {
                DisableWeaponColliders(_rightHandWeapon.GetComponent<WeaponManager>());
                return;
            }
            DisableWeaponColliders(_rightHandWeapon.GetComponent<WeaponManager>(),
            _leftHandWeapon.GetComponent<WeaponManager>(), hand);
        }

        #region Animation Events
        public void GrabWeapon() {
            if (startEquipped) return;
            EquipWeapon();
        }
        #endregion
    }
}