namespace BehaviorTree.Runtime.Nodes {
    public abstract class BasicNode {
        public string Name { get; protected set; }
        public NodeStatus Status { get; protected set; }
        public bool Initialized { get; protected set; }
        
        public NodeStatus UpdateNode() {
            if (!Initialized) {
                InitializeNode();
                Initialized = true;
            }
            Status = Tick();
            if (Status == NodeStatus.Running) return Status;
            ExitNode();
            Initialized = false;
            return Status;
        }
        
        protected abstract void InitializeNode();
        protected abstract NodeStatus Tick();
        protected abstract void ExitNode();
    }
}