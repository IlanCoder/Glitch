using System;
using System.Collections.Generic;
using Attacks.NPC;
using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using UnityEngine;

namespace AiBehaviorNodes.Combat {
    public class GetAvailableAttacksNode : LeafNode {
        [SerializeField] bool canRepeatLastAttack = true;
        List<NpcAttack> _filteredAttacks;
        
        protected override void InitializeNode() {
            _filteredAttacks = new List<NpcAttack>();
        }

        protected override NodeStatus Tick() {
            if (canRepeatLastAttack) {
                return !NpcAgent.combatController.TryGetAvailableAttacks(TreeBlackboard.targetCharacter, out _filteredAttacks)
                    ? NodeStatus.Failed
                    : NodeStatus.Succeeded;
            }
            return !NpcAgent.combatController.TryGetAvailableAttacks(TreeBlackboard.targetCharacter,
                TreeBlackboard.previousAttack, out _filteredAttacks)
                ? NodeStatus.Failed
                : NodeStatus.Succeeded;
        }

        protected override void ExitNode() {
            TreeBlackboard.availableAttacks.Clear();
            foreach (NpcAttack attack in _filteredAttacks) {
                TreeBlackboard.availableAttacks.Add(attack);
            }
            _filteredAttacks.Clear();
        }
    }
}