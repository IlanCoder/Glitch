﻿using AnimatorScripts.NPC;
using Attacks.NPC;
using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using UnityEngine;

namespace AiBehaviorNodes.Combat {
    public class AttackNode : LeafNode {
        NpcAttack _lastAttack;

        protected override void InitializeNode() {
            NpcAgent.isPerformingAction = true;
            InvokeNewAttackEvent.AttackStarted.AddListener(LoadNextAttackAnimation);
            _lastAttack = TreeBlackboard.AttackChain.Dequeue();
            NpcAgent.combatController.HandleAttackAnimation(_lastAttack);
        }

        protected override NodeStatus Tick() {
            try {
                return NpcAgent.isPerformingAction ? NodeStatus.Running : NodeStatus.Succeeded;
            } catch {
                return NodeStatus.Failed;
            }
        }

        protected override void ExitNode() {
            InvokeNewAttackEvent.AttackStarted.RemoveListener(LoadNextAttackAnimation);
            TreeBlackboard.lastAttackTime = Time.time;
            TreeBlackboard.lastAttackDownTime = _lastAttack.DownTime;
        }

        protected void LoadNextAttackAnimation() {
            if (TreeBlackboard.AttackChain.Count <= 0) return;
            _lastAttack = TreeBlackboard.AttackChain.Dequeue();
            NpcAgent.combatController.HandleAttackAnimation(_lastAttack, false);
        }
    }
}