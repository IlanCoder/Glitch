using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;

namespace AiBehaviorNodes.Movement {
    public class IdleNode : LeafNode{
        protected override void InitializeNode() {
            
        }

        protected override NodeStatus Tick() {
            NpcAgent.movementController.GoIdle();
            return NodeStatus.Succeeded;
        }

        protected override void ExitNode() {
            
        }
    }
}