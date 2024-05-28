namespace Data.BehaviorTree.Runtime.Nodes.Decorators {
    public class Repeater : DecoratorNode {
        public override NodeStatus Tick() {
            child.Tick();
            return NodeStatus.Running;
        }
    }
}