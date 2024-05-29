using Data.BehaviorTree.Runtime.Nodes.Decorators;

namespace BehaviorTree.Runtime.Nodes.Decorators {
    public class Succeeder : DecoratorNode{
        protected override void InitializeNode() { }

        protected override void ExitNode() { }
        
        protected override NodeStatus Tick() {
            return child.UpdateNode() == NodeStatus.Running ? NodeStatus.Running : NodeStatus.Succeeded;
        }
    }
}