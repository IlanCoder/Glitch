using System;
using DataContainers;
using Enums;
using UnityEngine;

namespace Characters{
	public class CharacterCombatController : MonoBehaviour {
		CharacterManager _manager;
		[SerializeField] protected Transform centerLockOnPivot;
		
		[Header("Combat Attributes")]
		[SerializeField] protected CombatTeam team;
		public CombatTeam Team => team;
		
		public Transform LockOnPivot => centerLockOnPivot;
		
		protected AttackType CurrentAttackType;

		protected virtual void Awake() {
			_manager = GetComponent<CharacterManager>();
		}

		#region Animation Events
		public virtual void EnableAttack(int hand = 0) {
			_manager.EquipmentManager.EnableWeaponAttack();
		}

		public virtual void DisableAttack(int hand = 0) {
			_manager.EquipmentManager.DisableWeaponAttack();
		}
		#endregion
	}
}
