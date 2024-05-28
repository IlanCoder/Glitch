namespace Data.BehaviorTree.Runtime.Nodes {
    public abstract class BasicNode {
        public string Name { get; protected set; }
        public virtual void Initialize() { }
        public virtual NodeStatus Tick() => NodeStatus.Failed;
        public virtual void Reset(){ }
    }
}