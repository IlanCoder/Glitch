using AnimatorScripts.NPC;
using Attacks.NPC;
using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace AiBehaviorNodes.Combat {
    public class AttackNode : LeafNode {
        NpcAttack _nextAttack;

        protected override void InitializeNode() {
            NpcAgent.isPerformingAction = true;
            InvokeNewAttackEvent.AttackStarted.AddListener(LoadNextAttackAnimation);
            InvokeNewAttackEvent.AttackFinished.AddListener(SetNextAttackValues);
            _nextAttack = TreeBlackboard.AttackChain.Dequeue();
            NpcAgent.combatController.HandleNextAttackAnimation(_nextAttack);
            NpcAgent.combatController.SetNewAttack(_nextAttack);
        }

        protected override NodeStatus Tick() {
            try {
                if (!NpcAgent.isPerformingAction) return NodeStatus.Succeeded;
                NpcAgent.combatController.HandleAttackRotationTracking(TreeBlackboard.targetCharacter);
                return NodeStatus.Running;
            } catch {
                return NodeStatus.Failed;
            }
        }

        protected override void ExitNode() {
            InvokeNewAttackEvent.AttackStarted.RemoveListener(LoadNextAttackAnimation);
            InvokeNewAttackEvent.AttackFinished.RemoveListener(SetNextAttackValues);
            TreeBlackboard.lastAttackTime = Time.time;
            TreeBlackboard.lastAttackDownTime = _nextAttack.DownTime;
        }

        protected void LoadNextAttackAnimation() {
            if (TreeBlackboard.AttackChain.Count <= 0) return;
            _nextAttack = TreeBlackboard.AttackChain.Dequeue();
            NpcAgent.combatController.HandleNextAttackAnimation(_nextAttack, false);
        }

        protected void SetNextAttackValues() {
            NpcAgent.combatController.SetNewAttack(_nextAttack);
        }
    }
}