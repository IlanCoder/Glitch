using Characters;
using Colliders;
using Items.Weapons;
using UnityEngine;

namespace Weapons{
	public class WeaponManager : MonoBehaviour {
		[SerializeField] WeaponDamageCollider damageCollider;

		public void SetWeapon(BasicWeapon weapon, CharacterManager wielder) {
			SetWeaponDamage(weapon);
			damageCollider.SetWielder(wielder);
		}
		
		public void SetWeaponDamage(BasicWeapon weapon) {
			damageCollider.SetDamage(weapon.Damage);
		}

		public void EnableDamageCollider() {
			damageCollider.EnableDamageCollider();
		}

		public void DisableDamageCollider() {
			damageCollider.DisableDamageCollider();
		}
	}
}
