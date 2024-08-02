using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using UnityEngine;

namespace AiBehaviorNodes.Movement {
    public class RotateToPursueDirectionNode : LeafNode {
        Vector3 _targetDirection;
        
        protected override void InitializeNode() {
            NpcAgent.animController.ApplyRootMotion(true);
            _targetDirection = NpcAgent.movementController.GetNavAgentRotation();
        }

        protected override NodeStatus Tick() {
            if (!TreeBlackboard.targetCharacter) return NodeStatus.Failed;
            if (NpcAgent.movementController.IsDirectionStraightAhead(_targetDirection))
                return NodeStatus.Succeeded;
            NpcAgent.movementController.RotateTowardsDirection(_targetDirection);
            return NodeStatus.Running;
        }

        protected override void ExitNode() {
            NpcAgent.animController.UpdateRotation(0);
        }
    }
}