using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;

namespace AiBehaviorNodes.Movement {
    public class WalkTowardsTargetNode : LeafNode {
        protected override void InitializeNode() {
            NpcAgent.movementController.StartChasing(NpcAgent.movementController.WalkStoppingDistance);
            NpcAgent.animController.ApplyRootMotion(true);
        }

        protected override NodeStatus Tick() {
            NpcAgent.movementController.ChaseTarget(TreeBlackboard.targetCharacter, 0.5f);
            if (!NpcAgent.movementController.IsPathComplete()) return NodeStatus.Failed;
            return NpcAgent.movementController.HasArrivedToStoppingDistance() ? NodeStatus.Succeeded : NodeStatus.Running;
        }

        protected override void ExitNode() {
            NpcAgent.movementController.StopChasing();
        }
    }
}