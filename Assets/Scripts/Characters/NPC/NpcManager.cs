using AIStates;
using UnityEngine;

namespace Characters.NPC {
	[RequireComponent(typeof(NpcMovementController)),
	 RequireComponent(typeof(NpcCombatController)),
	 RequireComponent(typeof(NpcAnimController))]
	public class NpcManager : CharacterManager {
		protected AIState CurrentState;
		protected AIState NextState;

		[HideInInspector] public NpcMovementController movementController;
		[HideInInspector] public NpcCombatController combatController;
		[HideInInspector] public NpcAnimController animController;
		
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
