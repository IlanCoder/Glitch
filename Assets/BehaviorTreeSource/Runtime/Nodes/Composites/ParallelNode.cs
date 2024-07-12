using System;
using Unity.VisualScripting.FullSerializer;

namespace BehaviorTreeSource.Runtime.Nodes.Composites {
    public class ParallelNode : CompositeNode {
        protected NodeStatus Status;
        protected override NodeStatus Tick() {
            Status = NodeStatus.Succeeded;
            foreach (BasicNode basicNode in Children) {
                switch (basicNode.UpdateNode()) {
                    case NodeStatus.Failed: return NodeStatus.Failed;
                    case NodeStatus.Running: 
                        Status = NodeStatus.Running;
                        break;
                }
            }
            return Status;
        }
    }
}