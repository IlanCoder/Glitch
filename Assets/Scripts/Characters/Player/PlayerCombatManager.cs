using Enums;
using Items.Weapons;
using UnityEngine;
using Weapons;

namespace Characters.Player{
	public class PlayerCombatManager : CharacterCombatManager {
		PlayerManager _manager;
		[HideInInspector] public BasicWeapon activeWeapon;
		[HideInInspector] public WeaponManager rightHandWeaponManager;
		[HideInInspector] public WeaponManager leftHandWeaponManager;

		protected override void Awake() {
			_manager = GetComponent<PlayerManager>();
		}

		public void PerformLightAttack() {
			if (activeWeapon == null) return;
			if (!_manager.isGrounded) return;
			if (!_manager.statsManager.CanPerformStaminaAction()) return;
			_manager.animManager.PlayAttackAnimation(AttackType.Light);
			CurrentAttackType = AttackType.Light;
			ApplyAttackModifiers();
		}

		void ApplyAttackModifiers() {
			activeWeapon.Damage.SetMultipliedDamage(1, 1);
			rightHandWeaponManager.SetWeaponDamage(activeWeapon);
			if(activeWeapon.DualWield) leftHandWeaponManager.SetWeaponDamage(activeWeapon);
		}

		#region Animation Events
		public virtual void DrainAttackStamina() {
			_manager.statsManager.UseStamina(activeWeapon.GetAttackStaminaCost(CurrentAttackType));
		}
        #endregion
	}
}