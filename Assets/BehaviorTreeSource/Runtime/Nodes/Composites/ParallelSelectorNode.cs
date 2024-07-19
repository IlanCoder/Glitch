namespace BehaviorTreeSource.Runtime.Nodes.Composites {
    public class ParallelSelectorNode : ParallelNode {
        protected override NodeStatus Tick() {
            Status = NodeStatus.Failed;
            EarlyExit = true;
            foreach (BasicNode basicNode in Children) {
                switch (basicNode.UpdateNode()) {
                    case NodeStatus.Succeeded: return NodeStatus.Succeeded;
                    case NodeStatus.Running: 
                        Status = NodeStatus.Running;
                        break;
                }
            }
            EarlyExit = false;
            return Status;
        }
    }
}