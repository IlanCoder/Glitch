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
		
		protected AttackType CurrentAttackType;

		protected virtual void Awake() { }
	}
}
