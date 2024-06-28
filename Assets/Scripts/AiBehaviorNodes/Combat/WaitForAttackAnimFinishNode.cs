using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves.General;

namespace AiBehaviorNodes.Combat {
    public class WaitForAttackAnimFinishNode : WaitNode {
        protected override void InitializeNode() {
            base.InitializeNode();
            Duration = TreeBlackboard.currentAttack.AttackAnimation.length;
        }

        protected override NodeStatus Tick() {
            return !NpcAgent.animController.IsAttackAnimationPlaying(TreeBlackboard.currentAttack)
                ? NodeStatus.Failed
                : base.Tick();
        }
    }
}