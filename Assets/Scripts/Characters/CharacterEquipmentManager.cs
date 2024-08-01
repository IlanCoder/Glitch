using UnityEngine;
using Weapons;

namespace Characters {
    public class CharacterEquipmentManager : MonoBehaviour {

        CharacterManager _manager;
        
        [Header("Equipment Locations")]
        [SerializeField] protected Transform rightHandWeaponLocation;
        [SerializeField] protected Transform leftHandWeaponLocation;
        
        protected virtual void Awake() {
            _manager = GetComponent<CharacterManager>();
        }
        
        protected void ActivateWeapon() {
            _manager.AnimController.PlayEquipAnimation();
        }

        protected void EnableWeaponColliders(WeaponManager rightWeapon, WeaponManager leftWeapon = null, int hand = 1) {
            switch (hand) {
                case 0:
                    rightWeapon.EnableDamageCollider();
                    if (leftWeapon) leftWeapon.EnableDamageCollider();
                    break;
                case 1:
                    rightWeapon.EnableDamageCollider();
                    break;
                case >=2:
                    if (leftWeapon) leftWeapon.EnableDamageCollider();
                    break;
            }
        }

        protected void DisableWeaponColliders(WeaponManager rightWeapon, WeaponManager leftWeapon = null, int hand = 1) {
            switch (hand) {
                case 0:
                    rightWeapon.DisableDamageCollider();
                    if (leftWeapon) leftWeapon.DisableDamageCollider();
                    break;
                case 1:
                    rightWeapon.DisableDamageCollider();
                    break;
                case >=2:
                    if (leftWeapon) leftWeapon.DisableDamageCollider();
                    break;
            }
        }

        public virtual void EnableWeaponAttack(int hand = 0) { }
        
        public virtual void DisableWeaponAttack(int hand = 0) { }
    }
}