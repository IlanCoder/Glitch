using Data.BehaviorTree.Runtime.Nodes.Decorators;

namespace BehaviorTree.Runtime.Nodes.Decorators {
    public class FiniteRepeater : Repeater {
        public uint Cycles { get; protected set; }
        protected uint CurrentCycle = 0;
        protected override void InitializeNode() {
            CurrentCycle = 0;
            base.InitializeNode();
        }

        protected override void ExitNode() { }

        protected override NodeStatus Tick() {
            if (CurrentCycle >= Cycles) return NodeStatus.Succeeded;
            CurrentCycle++;
            return base.Tick();
        }
    }
}