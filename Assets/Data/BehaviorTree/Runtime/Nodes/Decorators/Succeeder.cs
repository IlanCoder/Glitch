using UnityEngine.InputSystem.Switch;

namespace Data.BehaviorTree.Runtime.Nodes.Decorators {
    public class Succeeder : DecoratorNode{
        public override NodeStatus Tick() {
            return child.Tick() == NodeStatus.Running ? NodeStatus.Running : NodeStatus.Succeeded;
        }
    }
}