using BehaviorTreeSource.Runtime.Nodes.Decorators;

namespace AiBehaviorNodes.Conditionals {
    public class IsPerformingActionNode : ConditionalNode {
        protected override void InitializeNode() {
            
        }

        protected override void ExitNode() {
            
        }

        protected override bool CheckCondition() {
            return NpcAgent.isPerformingAction;
        }
    }
}