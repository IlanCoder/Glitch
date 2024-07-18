namespace BehaviorTreeSource.Runtime.Nodes.Decorators {
    public abstract class ConditionalNode : DecoratorNode {
        public bool invertCondition;
        
        protected override NodeStatus Tick() {
            if (CheckCondition() == !invertCondition) return Child.UpdateNode();
            if (Child.Status != NodeStatus.Running) return NodeStatus.Failed;
            Child.ExitNodeEarly();
            return NodeStatus.Failed;
        }

        protected abstract bool CheckCondition();
        
#if UNITY_EDITOR
        protected void OnValidate() {
            Description = invertCondition ? "Inverted" : "";
        }
#endif
    }
}