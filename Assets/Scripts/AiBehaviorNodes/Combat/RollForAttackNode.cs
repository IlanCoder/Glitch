using System.Collections.Generic;
using Attacks.NPC;
using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using UnityEngine;

namespace AiBehaviorNodes.Combat {
    public class RollForAttackNode : LeafNode {
        [SerializeField] bool canRepeatLastAttack = true;
        List<NpcAttack> _filteredAttacks;
        Queue<NpcAttack> _chosenAttacks;
        

        protected override void InitializeNode() {
            _filteredAttacks = new List<NpcAttack>();
            _chosenAttacks = new Queue<NpcAttack>();
            TreeBlackboard.previousAttack = TreeBlackboard.currentAttack;
        }

        protected override NodeStatus Tick() {
            if (canRepeatLastAttack) {
                if (!NpcAgent.combatController.TryGetAvailableAttacks(TreeBlackboard.targetCharacter,
                    out _filteredAttacks))
                    return NodeStatus.Failed;
            } else if (!NpcAgent.combatController.TryGetAvailableAttacks(TreeBlackboard.targetCharacter,
                       TreeBlackboard.previousAttack, out _filteredAttacks)) return NodeStatus.Failed;
            _chosenAttacks =
                NpcAgent.combatController.RollForAttackChain(NpcAgent.combatController.RollForAttack(_filteredAttacks));
            return _chosenAttacks.Count > 0 ? NodeStatus.Succeeded : NodeStatus.Failed;
        }

        protected override void ExitNode() {
            if (_chosenAttacks.Count > 0) {
                TreeBlackboard.currentAttack = _chosenAttacks.Peek();
            }
            TreeBlackboard.attackChain = _chosenAttacks;
            _filteredAttacks.Clear();
        }
    }
}