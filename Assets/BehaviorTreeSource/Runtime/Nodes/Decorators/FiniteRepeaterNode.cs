namespace BehaviorTreeSource.Runtime.Nodes.Decorators {
    public class FiniteRepeaterNode : RepeaterNode {
        public uint Cycles = 1;
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