using System;
using DataContainers;
using Enums;
using UnityEngine;

namespace Characters{
	public class CharacterCombatController : MonoBehaviour {
		[SerializeField] protected Transform centerLockOnPivot;
		[Header("Combat Attributes")]
		[SerializeField] protected CombatTeam team;
		public CombatTeam Team => team;
		
		public Transform LockOnPivot => centerLockOnPivot;
		
		public CharacterManager LockOnTarget { get; private set; }
		protected AttackType CurrentAttackType;

		protected virtual void Awake() { }

		public virtual void ChangeTarget(CharacterManager newTarget) {
			LockOnTarget = newTarget;
		}
	}
}
