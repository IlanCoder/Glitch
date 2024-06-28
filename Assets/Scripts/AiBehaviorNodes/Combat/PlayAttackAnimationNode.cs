using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace AiBehaviorNodes.Combat {
    public class PlayAttackAnimationNode : LeafNode {
        [SerializeField] bool firstInChain;
        protected override void InitializeNode() {
            
        }

        protected override NodeStatus Tick() {
            try {
                NpcAgent.combatController.HandleAttackAnimation(TreeBlackboard.currentAttack, firstInChain);
                return NodeStatus.Succeeded;
            } catch {
                return NodeStatus.Failed;
            }
        }

        protected override void ExitNode() {
            
        }
    }
}