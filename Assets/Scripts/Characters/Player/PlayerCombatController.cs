using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attacks;
using Attacks.Player;
using DataContainers;
using Enums;
using Items.Weapons;
using UnityEngine;
using Weapons;

namespace Characters.Player{
	public class PlayerCombatController : CharacterCombatController {
		PlayerManager _playerManager;
		
		public CharacterManager LockOnTarget { get; protected set; }

		int _comboIndex;
		int _currentAttackIndex;
		PlayerCombo _activeCombo;
		List<PlayerCombo> _activeWeaponCombos;
		List<PlayerCombo> _availableCombos;

		protected override void Awake() {
			base.Awake();
			_playerManager = GetComponent<PlayerManager>();
		}

		public void PerformNormalAttack(AttackType attackType) {
			if (activeWeapon == null) return;
			if (!_playerManager.isGrounded) return;
			if (!_playerManager.statsController.CanPerformStaminaAction()) return;
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
				_playerManager.animOverrider.OverrideCombos(_activeCombo.ComboAttacks, _comboIndex);
			}
			_playerManager.animController.PlayAttackAnimation(_comboIndex);
			CurrentAttack = _activeCombo.ComboAttacks[_comboIndex];
		}
		
		bool InputIsInCombos() {
			List<PlayerCombo> combosToRemove = new List<PlayerCombo>();
			foreach (PlayerCombo combo in _availableCombos) {
				if (combo.GetAttackInfo(_comboIndex).AttackType == CurrentAttackType) continue;
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
		
		void ApplyAttackModifiers() {
			float motionMultiplier = activeWeapon.GetAttackMotionMultiplier(_activeCombo, _comboIndex);
			float energyGain = activeWeapon.GetAttackEnergyGain(_activeCombo, _comboIndex);
			rightHandWeaponManager.SetWeaponDamageMultipliers(motionMultiplier);
			rightHandWeaponManager.SetWeaponEnergyGain(energyGain);
			if (!activeWeapon.DualWield) return;
			leftHandWeaponManager.SetWeaponDamageMultipliers(motionMultiplier);
			leftHandWeaponManager.SetWeaponEnergyGain(energyGain);
		}

		override public void SetActiveWeapon(BasicWeapon weapon, WeaponManager right, WeaponManager left = null) {
			base.SetActiveWeapon(weapon, right, left);
			_activeWeaponCombos = new List<PlayerCombo>(activeWeapon.Combos);
			ResetCombo();
		}

		public void ResetCombo() {
			_comboIndex = 0;
			_availableCombos = new List<PlayerCombo>(_activeWeaponCombos);
		}

		public void ChangeTarget(CharacterManager newTarget) {
			if (LockOnTarget) {
				LockOnTarget.onCharacterDeath.RemoveListener(WaitForTargetToDie);
			}
			if (newTarget) {
				newTarget.onCharacterDeath.AddListener(WaitForTargetToDie);
			}
			LockOnTarget = newTarget;
		}
		
		async void WaitForTargetToDie() {
			while (_playerManager.isPerformingAction) {
				await Task.Delay(10);
			}
			_playerManager.TryNewLockOn();
		}
		

		#region Animation Events
		override public void EnableAttack(int hand = 0) {
			_playerManager.statsController.UseStamina(activeWeapon.GetAttackStaminaCost(_activeCombo, _currentAttackIndex));
			base.EnableAttack(hand);
		}
        #endregion
	}
}
