using Characters.NPC;

namespace AIStates {
    public class AIState {
        protected NpcManager Manager;
        public virtual void EnterState(NpcManager manager) {
            Manager = manager;
        }
        
        public virtual AIState Tick() {
            return this;
        }

        public virtual void ExitState() {
            
        }
    }
}