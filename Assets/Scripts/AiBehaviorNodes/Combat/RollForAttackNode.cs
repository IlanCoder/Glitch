using System.Collections.Generic;
using Attacks.NPC;
using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using UnityEngine;

namespace AiBehaviorNodes.Combat {
    public class RollForAttackNode : LeafNode {
        [SerializeField] bool canRepeatLastAttack = true;
        List<NpcAttack> _filteredAttacks;
        NpcAttack _chosenAttack;
        
        protected override void InitializeNode() {
            _filteredAttacks = new List<NpcAttack>();
            _chosenAttack = null;
            TreeBlackboard.previousAttack = TreeBlackboard.currentAttack;
        }

        protected override NodeStatus Tick() {
            if (canRepeatLastAttack &&
                !NpcAgent.combatController.TryGetAvailableAttacks(TreeBlackboard.targetCharacter, out _filteredAttacks))
                return NodeStatus.Failed;
            if (!NpcAgent.combatController.TryGetAvailableAttacks(TreeBlackboard.targetCharacter,
                TreeBlackboard.previousAttack, out _filteredAttacks)) return NodeStatus.Failed;
            _chosenAttack = NpcAgent.combatController.RollForAttack(_filteredAttacks);
            return _chosenAttack is null ? NodeStatus.Failed : NodeStatus.Succeeded;
        }

        protected override void ExitNode() {
            TreeBlackboard.currentAttack = _chosenAttack;
            _filteredAttacks.Clear();
        }
    }
}