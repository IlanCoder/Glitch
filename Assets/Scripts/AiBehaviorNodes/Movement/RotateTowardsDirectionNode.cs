﻿using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using UnityEngine;

namespace AiBehaviorNodes.Movement {
    public class RotateTowardsDirectionNode : LeafNode {

        Vector3 targetDirection;
        
        protected override void InitializeNode() {
            NpcAgent.animController.ApplyRootMotion(true);
            targetDirection = NpcAgent.movementController.GetNextNavPosition() - NpcAgent.transform.position;
        }

        protected override NodeStatus Tick() {
            if (!TreeBlackboard.targetCharacter) return NodeStatus.Failed;
            if (NpcAgent.combatController.IsDirectionStraightAhead(targetDirection))
                return NodeStatus.Succeeded;
            NpcAgent.movementController.RotateTowardsDirection(targetDirection);
            return NodeStatus.Running;
        }

        protected override void ExitNode() {
            NpcAgent.animController.UpdateRotation(0);
        }
    }
}