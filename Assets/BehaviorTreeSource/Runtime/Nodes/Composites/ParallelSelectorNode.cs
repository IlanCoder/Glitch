namespace BehaviorTreeSource.Runtime.Nodes.Composites {
    public class ParallelSelectorNode : ParallelNode {
        protected override NodeStatus Tick() {
            Status = NodeStatus.Failed;
            foreach (BasicNode basicNode in Children) {
                switch (basicNode.UpdateNode()) {
                    case NodeStatus.Succeeded:
                        if (Status == NodeStatus.Running) break;
                        Status = NodeStatus.Succeeded;
                        break;
                    case NodeStatus.Running: 
                        Status = NodeStatus.Running;
                        break;
                }
            }
            return Status;
        }
    }
}