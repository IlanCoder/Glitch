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
            Manager.LockOn(_target);
        }

        public override AIState Tick() {
            return base.Tick();
        }

        public override void ExitState() {
            base.ExitState();
        }
    }
}
