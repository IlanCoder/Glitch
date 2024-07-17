namespace BehaviorTreeSource.Runtime.Nodes.Decorators {
    public abstract class ConditionalNode : DecoratorNode{
        protected override NodeStatus Tick() {
            if (CheckCondition()) return Child.UpdateNode();
            if (Child.Status != NodeStatus.Running) return NodeStatus.Failed;
            Child.ExitNodeEarly();
            return NodeStatus.Failed;
        }

        protected abstract bool CheckCondition();
    }
}