using UnityEngine;

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
    }
}