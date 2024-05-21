using Characters;
using Characters.NPC;
using UnityEngine;

namespace AIStates {
    public class PursueState : AIState {
        CharacterManager _target;

        public PursueState() => _target = null;
        public PursueState(CharacterManager target) => _target = target;
        
        public override void EnterState(NpcManager manager) {
            base.EnterState(manager);
            Manager.movementController.EnableNavMeshAgent();
        }

        public override AIState Tick() {
            Manager.movementController.SetNavMeshDestination(_target.transform.position);
            if (Manager.movementController.HasArrivedToLockOnRange()) return new IdleState();
            return this;
        }

        public override void ExitState() {
            base.ExitState();
        }
    }
}
