using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves.General;

namespace AiBehaviorNodes.Combat {
    public class AttackNode : WaitNode {
        bool _firstInChain;
        
        protected override void InitializeNode() {
            _firstInChain = true;
            Duration = TreeBlackboard.currentAttack.AttackAnimation.length;
        }

        protected override NodeStatus Tick() {
            try {
                NpcAgent.combatController.HandleAttackAnimation(TreeBlackboard.currentAttack, _firstInChain);
                return NpcAgent.animController.IsAttackAnimationPlaying(TreeBlackboard.currentAttack)
                    ? base.Tick()
                    : NodeStatus.Failed;
            } catch {
                return NodeStatus.Failed;
            }
        }

        protected override void ExitNode() {
            
        }
    }
}