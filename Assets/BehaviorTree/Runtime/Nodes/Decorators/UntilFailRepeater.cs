namespace BehaviorTree.Runtime.Nodes.Decorators {
    public class UntilFailRepeater : Repeater {
        protected override void InitializeNode() { }

        protected override void ExitNode() { }
        
        protected override NodeStatus Tick() {
            return child.UpdateNode() == NodeStatus.Failed ? NodeStatus.Succeeded : NodeStatus.Running;
        }
    }
}