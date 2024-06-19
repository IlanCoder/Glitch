using Characters;

namespace BehaviorTreeSource.Runtime.Nodes.Leaves.Movement {
    public class RotateTowardsTargetNode : LeafNode {
        protected override void InitializeNode() {
        }

        protected override NodeStatus Tick() {
            if (!TreeBlackboard.targetCharacter) return NodeStatus.Failed;
            if (NpcAgent.combatController.IsTargetStraightAhead(TreeBlackboard.targetCharacter)) {
                NpcAgent.animController.UpdateRotation(0);
                return NodeStatus.Succeeded;
            }
            NpcAgent.movementController.RotateTowardsTarget(TreeBlackboard.targetCharacter);
            return NodeStatus.Running;
        }

        protected override void ExitNode() {
        }
    }
}