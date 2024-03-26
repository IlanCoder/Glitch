using Colliders;
using Items.Weapons;
using UnityEngine;

namespace Weapons{
	public class WeaponManager : MonoBehaviour {
		[SerializeField] DamageCollider damageCollider;

		public void SetWeaponDamage(BasicWeapon weapon) {
			damageCollider.SetDamage(weapon.Damage);
		}
	}
}
