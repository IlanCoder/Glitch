using System;

namespace Data.BehaviorTree.Runtime.Nodes.Decorators {
    public class InverterNode : DecoratorNode {
        public override NodeStatus Tick() {
            switch (child.Tick()) {
                case NodeStatus.Failed: return NodeStatus.Succeeded;
                case NodeStatus.Succeeded: return NodeStatus.Failed;
                case NodeStatus.Running: return NodeStatus.Running;
                default:
                    throw new ArgumentOutOfRangeException(Name);
            }
        }
    }
}