namespace BehaviorTreeSource.Runtime.Nodes.Decorators {
    public abstract class ConditionalNode : DecoratorNode{
        protected override NodeStatus Tick() {
            return CheckCondition() ? Child.UpdateNode() : NodeStatus.Failed;
        }

        protected abstract bool CheckCondition();
    }
}