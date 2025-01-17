﻿using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;

namespace AiBehaviorNodes.Movement {
    public class PursueTargetNode : LeafNode {
        protected override void InitializeNode() {
            NpcAgent.movementController.StartChasing(NpcAgent.movementController.PursueStoppingDistance);
            NpcAgent.animController.ApplyRootMotion(true);
        }

        protected override NodeStatus Tick() {
            NpcAgent.movementController.ChaseTarget(TreeBlackboard.targetCharacter);
            if (!NpcAgent.movementController.IsPathComplete()) return NodeStatus.Failed;
            return NpcAgent.movementController.HasArrivedToStoppingDistance() ? NodeStatus.Succeeded : NodeStatus.Running;
        }

        protected override void ExitNode() {
            NpcAgent.movementController.StopChasing();
        }
    }
}