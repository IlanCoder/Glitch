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
using UnityEngine.Events;
using Weapons;

namespace Characters.Player{
	public class PlayerCombatController : CharacterCombatController {
		PlayerManager _playerManager;
		
		public CharacterManager LockOnTarget { get; protected set; }

		[HideInInspector] public UnityEvent<CharacterManager> onLockOnTargetChange;
		
		int _comboIndex;
		int _currentAttackIndex;
		int _nextAttackIndex;
		bool _canInputQueue;
		CharacterAttack _nextAttack;
		PlayerCombo _activeCombo;
		List<PlayerCombo> _activeWeaponCombos;
		List<PlayerCombo> _availableCombos;
		public bool IsAttacking { get; protected set; }

		protected override void Awake() {
			base.Awake();
			_playerManager = GetComponent<PlayerManager>();
			onAttackStarted.AddListener(SetNextAttack);
		}

		void SetNextAttack() {
			CurrentAttack = _nextAttack;
			_currentAttackIndex = _nextAttackIndex;
			_canInputQueue = false;
		}

		public void TryPerformAttack(AttackType attackType) {
			if (activeWeapon == null) return;
			if (!_playerManager.isGrounded) return;
			if (IsAttacking) {
				if (!_canInputQueue) return;
				if (_comboIndex != _currentAttackIndex && attackType == _nextAttack.AttackType) return;
				_comboIndex = _currentAttackIndex + 1;
			}
			if (!CanContinueCombo()) return;
			if (!InputIsInCombos(attackType)) return;
			PerformNormalAttack();
		}
		
		public void PerformNormalAttack() {
			if (!_playerManager.statsController.HasStamina()) return;
			IsAttacking = true;
			_nextAttackIndex = _comboIndex;
			HandleAttackAnimation();
			_nextAttack = _activeCombo.ComboAttacks[_nextAttackIndex];
		}

		void HandleAttackAnimation() {
			if (_activeCombo != _availableCombos[0]) {
				_activeCombo = _availableCombos[0];
				_playerManager.animOverrider.OverrideCombos(_activeCombo.ComboAttacks, _nextAttackIndex);
			}
			_playerManager.animController.PlayAttackAnimation(_nextAttackIndex);
		}

		#region Combo
		bool InputIsInCombos(AttackType inputType) {
			List<PlayerCombo> combosToRemove = new List<PlayerCombo>();
			foreach (PlayerCombo combo in _availableCombos) {
				if (combo.GetAttackInfo(_comboIndex).AttackType == inputType) continue;
				combosToRemove.Add(combo);
			}
			foreach (PlayerCombo comboToRemove in combosToRemove) {
				_availableCombos.Remove(comboToRemove);
			}
			return _availableCombos.Count > 0;
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
		
		public void ResetCombo() {
			_comboIndex = 0;
			_availableCombos = new List<PlayerCombo>(_activeWeaponCombos);
			IsAttacking = false;
		}
  #endregion
		
		override public void SetActiveWeapon(BasicWeapon weapon, WeaponManager right, WeaponManager left = null) {
			base.SetActiveWeapon(weapon, right, left);
			_activeWeaponCombos = new List<PlayerCombo>(activeWeapon.Combos);
			ResetCombo();
		}

		public void ChangeTarget(CharacterManager newTarget) {
			if (LockOnTarget) {
				LockOnTarget.onCharacterDeath.RemoveListener(WaitForTargetToDie);
			}
			if (newTarget) {
				newTarget.onCharacterDeath.AddListener(WaitForTargetToDie);
			}
			LockOnTarget = newTarget;
			onLockOnTargetChange?.Invoke(LockOnTarget);
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

		public void EnableInputQueue() {
			_canInputQueue = true;
		}
        #endregion
	}
}
