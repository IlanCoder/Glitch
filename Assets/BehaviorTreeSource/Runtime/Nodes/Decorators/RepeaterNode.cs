namespace BehaviorTreeSource.Runtime.Nodes.Decorators {
    public class RepeaterNode : DecoratorNode {
        
        protected override void InitializeNode() { }

        protected override void ExitNode() { }
        
        protected override NodeStatus Tick() {
            Child.UpdateNode();
            return NodeStatus.Running;
        }
    }
}