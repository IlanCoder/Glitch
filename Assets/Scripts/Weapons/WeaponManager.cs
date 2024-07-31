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
			damageCollider.SetDamage(weapon.Damage);
			damageCollider.SetWielder(wielder);
		}

		public void SetWeaponDamageMultipliers(float motionMultiplier, float attackMultiplier = 1) {
			damageCollider.SetAttackModifier(motionMultiplier, attackMultiplier);
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
