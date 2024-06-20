using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;

namespace AiBehaviorNodes.Movement {
    public class RotateTowardsTargetNode : LeafNode {
        protected override void InitializeNode() {
            NpcAgent.animController.ApplyRootMotion(true);
        }

        protected override NodeStatus Tick() {
            if (!TreeBlackboard.targetCharacter) return NodeStatus.Failed;
            if (NpcAgent.combatController.IsTargetStraightAhead(TreeBlackboard.targetCharacter))
                return NodeStatus.Succeeded;
            NpcAgent.movementController.RotateTowardsTarget(TreeBlackboard.targetCharacter);
            return NodeStatus.Running;
        }

        protected override void ExitNode() {
            NpcAgent.animController.UpdateRotation(0);
        }
    }
}