namespace Data.BehaviorTree.Runtime.Nodes.Decorators {
    public class FiniteRepeater : Repeater {
        public uint Cycles { get; protected set; }
        protected uint CurrentCycle = 0;
        public override void Initialize() {
            CurrentCycle = 0;
            base.Initialize();
        }

        public override NodeStatus Tick() {
            if (CurrentCycle >= Cycles) return NodeStatus.Succeeded;
            CurrentCycle++;
            return base.Tick();
        }

        public override void Reset() {
            CurrentCycle = 0;
            base.Reset();
        }
    }
}