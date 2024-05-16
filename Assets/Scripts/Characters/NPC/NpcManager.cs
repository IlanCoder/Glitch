using AIStates;
using UnityEngine;

namespace Characters.NPC {
	[RequireComponent(typeof(NpcCombatController))]
	public class NpcManager : CharacterManager {
		protected AIState CurrentState;
		protected AIState NextState;
		
		[HideInInspector]public NpcCombatController combatController;
		
		public override CharacterCombatController CombatController => combatController;

		protected override void Awake() {
			base.Awake();
			combatController = GetComponent<NpcCombatController>();
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
			combatController.ChangeTarget(target);
		}
	}
}
