namespace BehaviorTree.Runtime.Nodes.Decorators {
    public abstract class DecoratorNode : BasicNode {
        protected BasicNode child;

        public virtual void SetChild(BasicNode node) => child = node;
    }
}