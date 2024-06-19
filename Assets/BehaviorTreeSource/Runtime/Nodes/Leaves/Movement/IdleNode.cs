namespace BehaviorTreeSource.Runtime.Nodes.Leaves.Movement {
    public class IdleNode : LeafNode{
        protected override void InitializeNode() {
            NpcAgent.movementController.GoIdle();
        }

        protected override NodeStatus Tick() {
            return NodeStatus.Succeeded;
        }

        protected override void ExitNode() {
            
        }
    }
}