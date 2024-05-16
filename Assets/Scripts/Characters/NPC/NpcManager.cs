using AIStates;
using UnityEngine;

namespace Characters.NPC {
	[RequireComponent(typeof(NpcCombatManager))]
	public class NpcManager : CharacterManager {
		protected AIState CurrentState;
		protected AIState NextState;
		
		[HideInInspector]public NpcCombatManager combatManager;
		
		public override CharacterCombatManager CombatManager => combatManager;

		protected override void Awake() {
			base.Awake();
			combatManager = GetComponent<NpcCombatManager>();
			CurrentState = new IdleState();
			CurrentState.EnterState(this);
		}


		protected override void Update() {
			ProcessCurrentState();
		}
		
		protected virtual void ProcessCurrentState() {
			if (CurrentState == null) return;
			NextState = CurrentState.Tick();
			if (NextState == CurrentState) return;
			CurrentState.ExitState();
			CurrentState = NextState;
			CurrentState.EnterState(this);
		}

		public virtual void LockOn(CharacterManager target) {
			combatManager.ChangeTarget(target);
		}
	}
}
