namespace Data.BehaviorTree.Runtime.Nodes.Decorators {
    public class UntilFailRepeater : Repeater {
        public override NodeStatus Tick() {
            return child.Tick() == NodeStatus.Failed ? NodeStatus.Succeeded : NodeStatus.Running;
        }
    }
}