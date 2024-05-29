using BehaviorTree.Runtime.Nodes;

namespace Data.BehaviorTree.Runtime.Nodes.Decorators {
    public abstract class DecoratorNode : BasicNode {
        protected BasicNode child;
    }
}