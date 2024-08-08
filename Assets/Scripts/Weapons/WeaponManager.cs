using System;
using Characters;
using Characters.NPC;
using Colliders;
using DataContainers;
using Items.Weapons;
using UnityEngine;

namespace Weapons{
	public class WeaponManager : MonoBehaviour {
		[SerializeField] WeaponDamageCollider damageCollider;

		public void SetWeapon(BasicWeapon weapon, CharacterManager wielder) {
			damageCollider.SetDamage(weapon.damage);
			damageCollider.SetWielder(wielder);
		}

		public void SetWeaponEnergyGain(float energyGain) {
			damageCollider.SetEnergyGain(energyGain);
		}

		public void EnableDamageCollider() {
			damageCollider.EnableDamageCollider();
		}

		public void DisableDamageCollider() {
			damageCollider.DisableDamageCollider();
		}
	}
}
