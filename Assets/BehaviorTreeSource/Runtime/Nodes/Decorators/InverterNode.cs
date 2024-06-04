using System;

namespace BehaviorTreeSource.Runtime.Nodes.Decorators {
    public class InverterNode : DecoratorNode {
        protected override void InitializeNode() { }

        protected override void ExitNode() { }
        
        protected override NodeStatus Tick() {
            switch (Child.UpdateNode()) {
                case NodeStatus.Failed: return NodeStatus.Succeeded;
                case NodeStatus.Succeeded: return NodeStatus.Failed;
                case NodeStatus.Running: return NodeStatus.Running;
                default:
                    throw new ArgumentOutOfRangeException(NodeName);
            }
        }
    }
}