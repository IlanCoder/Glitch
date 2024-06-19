namespace BehaviorTreeSource.Runtime.Nodes.Leaves.Movement {
    public class ChaseTargetNode : LeafNode {
        protected override void InitializeNode() {
            NpcAgent.movementController.StartChasing();
            NpcAgent.animController.ApplyRootMotion(true);
        }

        protected override NodeStatus Tick() {
            NpcAgent.movementController.ChaseTarget(TreeBlackboard.targetCharacter);
            if (!NpcAgent.movementController.IsPathComplete()) return NodeStatus.Failed;
            return NpcAgent.movementController.HasArrivedToLockOnRange() ? NodeStatus.Succeeded : NodeStatus.Running;
        }

        protected override void ExitNode() {
            
        }
    }
}