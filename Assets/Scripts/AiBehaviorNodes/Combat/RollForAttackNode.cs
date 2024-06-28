using Attacks.NPC;
using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;

namespace AiBehaviorNodes.Combat {
    public class RollForAttackNode : LeafNode {
        NpcAttack chosenAttack;
        protected override void InitializeNode() {
            chosenAttack = null;
            TreeBlackboard.previousAttack = TreeBlackboard.currentAttack;
        }

        protected override NodeStatus Tick() {
            chosenAttack = NpcAgent.combatController.RollForAttack(TreeBlackboard.availableAttacks);
            return chosenAttack is null ? NodeStatus.Failed : NodeStatus.Succeeded;
        }

        protected override void ExitNode() {
            TreeBlackboard.currentAttack = chosenAttack;
        }
    }
}