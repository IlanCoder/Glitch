using BehaviorTreeSource.Runtime.Nodes.Decorators;
using UnityEngine;

namespace AiBehaviorNodes.Conditionals {
    public class IsAttackDownTimeOverNode : ConditionalNode {
        protected override void InitializeNode() {
            
        }

        protected override void ExitNode() {
            
        }

        protected override bool CheckCondition() {
            if (TreeBlackboard.lastAttackDownTime <= 0) return true;
            return Time.time - TreeBlackboard.lastAttackTime > TreeBlackboard.lastAttackDownTime;
        }
    }
}