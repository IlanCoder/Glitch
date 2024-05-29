using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attacks;
using DataContainers;
using Enums;
using Items.Weapons;
using UnityEngine;
using Weapons;

namespace Characters.Player{
	public class PlayerCombatController : CharacterCombatController {
		PlayerManager _manager;
		[HideInInspector] public BasicWeapon activeWeapon;
		[HideInInspector] public WeaponManager rightHandWeaponManager;
		[HideInInspector] public WeaponManager leftHandWeaponManager;

		int _comboIndex;
		int _currentAttackIndex;
		PlayerCombo _activeCombo;
		List<PlayerCombo> _activeWeaponCombos;
		List<PlayerCombo> _availableCombos;

		protected override void Awake() {
			_manager = GetComponent<PlayerManager>();
		}

		public void PerformNormalAttack(AttackType attackType) {
			if (activeWeapon == null) return;
			if (!_manager.isGrounded) return;
			if (!_manager.statsController.CanPerformStaminaAction()) return;
			CurrentAttackType = attackType;
			if (!InputIsInCombos()) return;
			HandleAttackAnimation();
			ApplyAttackModifiers();
			_currentAttackIndex = _comboIndex;
			_comboIndex++;
			if (CanContinueCombo()) return;
			ResetCombo();
		}

		void HandleAttackAnimation() {
			if (_activeCombo != _availableCombos[0]) {
				_activeCombo = _availableCombos[0];
				_manager.animOverrider.OverrideCombos(_activeCombo.ComboAttacks, _comboIndex);
			}
			_manager.animController.PlayAttackAnimation(_comboIndex);
		}
		
		bool InputIsInCombos() {
			List<PlayerCombo> combosToRemove = new List<PlayerCombo>();
			foreach (PlayerCombo combo in _availableCombos) {
				if (combo.GetAttackInfo(_comboIndex).Input == CurrentAttackType) continue;
				combosToRemove.Add(combo);
			}
			foreach (PlayerCombo comboToRemove in combosToRemove) {
				_availableCombos.Remove(comboToRemove);
			}
			if (_availableCombos.Count > 0) return true;
			ResetCombo();
			return false;
		}

		bool CanContinueCombo() {
			List<PlayerCombo> combosToRemove = new List<PlayerCombo>();
			foreach (PlayerCombo combo in _availableCombos) {
				if (combo.ComboLength > _comboIndex) continue;
				combosToRemove.Add(combo);
			}
			foreach (PlayerCombo comboToRemove in combosToRemove) {
				_availableCombos.Remove(comboToRemove);
			}
			return _availableCombos.Count > 0;
		}

		public void SetActiveWeapon(BasicWeapon weapon) {
			activeWeapon = weapon;
			_activeWeaponCombos = new List<PlayerCombo>(activeWeapon.Combos);
			ResetCombo();
		}

		public void ResetCombo() {
			_comboIndex = 0;
			_availableCombos = new List<PlayerCombo>(_activeWeaponCombos);
		}

		void ApplyAttackModifiers() {
			float motionMultiplier = activeWeapon.GetAttackMotionMultiplier(_activeCombo, _comboIndex);
			float energyGain = activeWeapon.GetAttackEnergyGain(_activeCombo, _comboIndex);
			rightHandWeaponManager.SetWeaponDamageMultipliers(motionMultiplier);
			rightHandWeaponManager.SetWeaponEnergyGain(energyGain);
			if (!activeWeapon.DualWield) return;
			leftHandWeaponManager.SetWeaponDamageMultipliers(motionMultiplier);
			leftHandWeaponManager.SetWeaponEnergyGain(energyGain);
		}

		public override void ChangeTarget(CharacterManager newTarget) {
			if (LockOnTarget) {
				LockOnTarget.onCharacterDeath.RemoveListener(WaitForTargetToDie);
			}
			if (newTarget) {
				newTarget.onCharacterDeath.AddListener(WaitForTargetToDie);
			}
			base.ChangeTarget(newTarget);
		}
		
		async void WaitForTargetToDie() {
			while (_manager.isPerformingAction) {
				await Task.Delay(10);
			}
			_manager.TryNewLockOn();
		}

		#region Animation Events
		public virtual void DrainAttackStamina() {
			_manager.statsController.UseStamina(activeWeapon.GetAttackStaminaCost(_activeCombo, _currentAttackIndex));
			_manager.equipmentManager.EnableWeaponColliders();
		}
        #endregion
	}
}