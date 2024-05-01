using System;
using Enums;
using UnityEngine;

namespace Characters{
	public class CharacterCombatManager : MonoBehaviour {
		[SerializeField] protected Transform centerLockOnPivot;
		public Transform LockOnPivot => centerLockOnPivot;
		
		public CharacterManager LockOnTarget { get; private set; }
		protected AttackType CurrentAttackType;
		
		protected virtual void Awake() {
		}

		public virtual void ChangeTarget(CharacterManager newTarget) {
			LockOnTarget = newTarget;
		}
	}
}
