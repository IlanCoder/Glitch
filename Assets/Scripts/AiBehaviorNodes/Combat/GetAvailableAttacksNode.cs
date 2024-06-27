using System.Collections.Generic;
using Attacks.NPC;
using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;

namespace AiBehaviorNodes.Combat {
    public class GetAvailableAttacksNode : LeafNode {
        List<NpcAttack> filteredAttacks;
        protected override void InitializeNode() {
            filteredAttacks = new List<NpcAttack>();
        }

        protected override NodeStatus Tick() {
            return !NpcAgent.combatController.TryGetAvailableAttacks(TreeBlackboard.targetCharacter, out filteredAttacks)
                ? NodeStatus.Failed
                : NodeStatus.Succeeded;
        }

        protected override void ExitNode() {
            TreeBlackboard.availableAttacks = filteredAttacks;
            filteredAttacks.Clear();
        }
    }
}