using AIStates;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.NPC {
	[RequireComponent(typeof(NavMeshAgent)),
	 RequireComponent(typeof(NpcMovementController)),
	 RequireComponent(typeof(NpcCombatController)),
	 RequireComponent(typeof(NpcAnimController))]
	public class NpcManager : CharacterManager {
		protected AIState CurrentState;
		protected AIState NextState;

		[HideInInspector] public NpcMovementController movementController;
		[HideInInspector] public NpcCombatController combatController;
		[HideInInspector] public NpcAnimController animController;
		
		public override CharacterMovementController MovementController => movementController;
		public override CharacterCombatController CombatController => combatController;
		public override CharacterAnimController AnimController => animController;

		protected override void Awake() {
			base.Awake();
			movementController = GetComponent<NpcMovementController>();
			combatController = GetComponent<NpcCombatController>();
			animController = GetComponent<NpcAnimController>();
		}

		protected void Start() {
			CurrentState = new IdleState();
			CurrentState.EnterState(this);
		}

		protected virtual void FixedUpdate() {
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
	}
}
