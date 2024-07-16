using AnimatorScripts.NPC;
using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using BehaviorTreeSource.Runtime.Nodes.Leaves.General;

namespace AiBehaviorNodes.Combat {
    public class AttackNode : LeafNode {

        protected override void InitializeNode() {
            NpcAgent.isPerformingAction = true;
            InvokeNewAttackEvent.AttackStarted.AddListener(LoadNextAttackAnimation);
            NpcAgent.combatController.HandleAttackAnimation(TreeBlackboard.AttackChain.Dequeue());
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
        }

        protected void LoadNextAttackAnimation() {
            if (TreeBlackboard.AttackChain.Count <= 0) return;
            NpcAgent.combatController.HandleAttackAnimation(TreeBlackboard.AttackChain.Dequeue(), false);
        }
    }
}