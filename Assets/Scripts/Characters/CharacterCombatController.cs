using System;
using Attacks;
using DataContainers;
using Enums;
using Items.Weapons;
using UnityEngine;
using UnityEngine.Events;
using Weapons;

namespace Characters{
	public class CharacterCombatController : MonoBehaviour {
		CharacterManager _manager;
		[HideInInspector] public BasicWeapon activeWeapon;
		[HideInInspector] public WeaponManager rightHandWeaponManager;
		[HideInInspector] public WeaponManager leftHandWeaponManager;

		[Header("Lock On Pivots")]
		[SerializeField] protected Transform centerLockOnPivot;
		[SerializeField] protected Transform uiLockOnPivotRef;
		
		[Header("Combat Attributes")]
		[SerializeField] protected CombatTeam team;

		[HideInInspector] public UnityEvent onAttackStarted;
		[HideInInspector] public UnityEvent onAttackFinished;

		[Header("Invulnerability Times")]
		[SerializeField, Min(0.1f)] protected float rollIFramesTime;
		float _iFramesStartTime;

		public CombatTeam Team => team;
		public Transform LockOnPivot => centerLockOnPivot;
		public Transform UILockOnPivotRef => uiLockOnPivotRef;

		protected CharacterAttack CurrentAttack;

		protected virtual void Awake() {
			_manager = GetComponent<CharacterManager>();
		}

		public void OverrideTeam(CombatTeam newTeam) {
			team = newTeam;
		}

		public void HandleInvulnerability() {
			if (!_manager.isInvulnerable) return;
			if (Time.time - _iFramesStartTime < rollIFramesTime) return;
			_manager.isInvulnerable = false;
		}

		public virtual void SetActiveWeapon(BasicWeapon weapon, WeaponManager right, WeaponManager left = null) {
			activeWeapon = weapon;
			rightHandWeaponManager = right;
			leftHandWeaponManager = left;
		}

		protected void ApplyAttackModifiers() {
			activeWeapon.SetAttackDamage(CurrentAttack);
			float energyGain = activeWeapon.GetAttackEnergyGain(CurrentAttack); ;
			rightHandWeaponManager.SetWeaponEnergyGain(energyGain);
			if (!activeWeapon.DualWield) return;
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
		
		public void EnableRotationTracking() {
			_manager.rotationLocked = false;
		}
        
		public void DisableRotationTracking() {
			_manager.rotationLocked = true;
		}
		#endregion

		public void MakeInvulnerable() {
			_manager.isInvulnerable = true;
			_iFramesStartTime = Time.time;
		}
	}
}
