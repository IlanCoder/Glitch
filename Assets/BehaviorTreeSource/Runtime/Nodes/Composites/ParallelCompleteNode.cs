namespace BehaviorTreeSource.Runtime.Nodes.Composites {
    public class ParallelCompleteNode : ParallelNode {
        protected override NodeStatus Tick() {
            Status = NodeStatus.Running;
            foreach (BasicNode basicNode in Children) {
                switch (basicNode.UpdateNode()) {
                    case NodeStatus.Failed: return NodeStatus.Failed;
                    case NodeStatus.Succeeded: return NodeStatus.Succeeded;
                    case NodeStatus.Running: continue;
                }
            }
            return Status;
        }
    }
}