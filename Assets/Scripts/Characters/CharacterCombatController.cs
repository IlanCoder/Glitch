using System;
using Attacks;
using DataContainers;
using Enums;
using Items.Weapons;
using UnityEngine;
using Weapons;

namespace Characters{
	public class CharacterCombatController : MonoBehaviour {
		CharacterManager _manager;
		[HideInInspector] public BasicWeapon activeWeapon;
		[HideInInspector] public WeaponManager rightHandWeaponManager;
		[HideInInspector] public WeaponManager leftHandWeaponManager;

		[SerializeField] protected Transform centerLockOnPivot;
		
		[Header("Combat Attributes")]
		[SerializeField] protected CombatTeam team;
		public CombatTeam Team => team;
		
		public Transform LockOnPivot => centerLockOnPivot;
		
		protected AttackType CurrentAttackType;
		
		protected CharacterAttack CurrentAttack;

		protected virtual void Awake() {
			_manager = GetComponent<CharacterManager>();
		}

		public virtual void SetActiveWeapon(BasicWeapon weapon, WeaponManager right, WeaponManager left = null) {
			activeWeapon = weapon;
			rightHandWeaponManager = right;
			leftHandWeaponManager = left;
		}
		
		protected void ApplyAttackModifiers() {
			float motionMultiplier = activeWeapon.GetAttackMotionMultiplier(CurrentAttack);
			float energyGain = activeWeapon.GetAttackEnergyGain(CurrentAttack);
			rightHandWeaponManager.SetWeaponDamageMultipliers(motionMultiplier);
			rightHandWeaponManager.SetWeaponEnergyGain(energyGain);
			if (!activeWeapon.DualWield) return;
			leftHandWeaponManager.SetWeaponDamageMultipliers(motionMultiplier);
			leftHandWeaponManager.SetWeaponEnergyGain(energyGain);
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

		protected void EnableWeaponAttack(int hand = 0) {
			if (!activeWeapon) return;
			if (!activeWeapon.DualWield) {
				EnableWeaponColliders(rightHandWeaponManager);
				return;
			}
			EnableWeaponColliders(rightHandWeaponManager, leftHandWeaponManager, hand);
		}

		protected void DisableWeaponAttack(int hand = 0) {
			if (!activeWeapon) return;
			if (!activeWeapon.DualWield) {
				DisableWeaponColliders(rightHandWeaponManager);
				return;
			}
			DisableWeaponColliders(rightHandWeaponManager, leftHandWeaponManager, hand);
		}
		
		
		#region Animation Events
		public virtual void EnableAttack(int hand = 0) {
			ApplyAttackModifiers();
			EnableWeaponAttack(hand);
			_manager.SFxController.PlayAttackSwingSFx(CurrentAttack.SwingAudioClip);
		}

		public virtual void DisableAttack(int hand = 0) {
			DisableWeaponAttack(hand);
		}
		#endregion
	}
}
