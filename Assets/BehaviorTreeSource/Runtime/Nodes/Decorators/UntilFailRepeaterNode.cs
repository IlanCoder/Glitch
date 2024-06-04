namespace BehaviorTreeSource.Runtime.Nodes.Decorators {
    public class UntilFailRepeaterNode : RepeaterNode {
        protected override void InitializeNode() { }

        protected override void ExitNode() { }
        
        protected override NodeStatus Tick() {
            return Child.UpdateNode() == NodeStatus.Failed ? NodeStatus.Succeeded : NodeStatus.Running;
        }
    }
}