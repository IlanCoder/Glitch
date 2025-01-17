﻿using System.Numerics;
using BehaviorTreeSource.Runtime.Nodes.Decorators;

namespace AiBehaviorNodes.Conditionals {
    public class IsTargetInCombatRangeNode : ConditionalNode {
        protected override void InitializeNode() {
        }

        protected override void ExitNode() {
            
        }

        protected override bool CheckCondition() {
            if (TreeBlackboard.targetCharacter is null) return false;
            return NpcAgent.movementController.PursueStoppingDistance > UnityEngine.Vector3.Distance(NpcAgent.transform.position,
            TreeBlackboard.targetCharacter.transform.position);
        }
    }
}