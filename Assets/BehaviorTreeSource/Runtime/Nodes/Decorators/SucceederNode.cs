namespace BehaviorTreeSource.Runtime.Nodes.Decorators {
    public class SucceederNode : DecoratorNode{
        protected override void InitializeNode() { }

        protected override void ExitNode() { }
        
        protected override NodeStatus Tick() {
            return Child.UpdateNode() == NodeStatus.Running ? NodeStatus.Running : NodeStatus.Succeeded;
        }
    }
}