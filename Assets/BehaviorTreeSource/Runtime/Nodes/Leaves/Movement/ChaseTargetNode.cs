namespace BehaviorTreeSource.Runtime.Nodes.Leaves.Movement {
    public class ChaseTargetNode : LeafNode {
        protected override void InitializeNode() {
            Character.movementController.StartChasing();
            Character.animController.ApplyRootMotion(true);
        }

        protected override NodeStatus Tick() {
            Character.movementController.Chase();
            if (!Character.movementController.IsPathComplete()) return NodeStatus.Failed;
            return Character.movementController.HasArrivedToLockOnRange() ? NodeStatus.Succeeded : NodeStatus.Running;
        }

        protected override void ExitNode() {
            
        }
    }
}