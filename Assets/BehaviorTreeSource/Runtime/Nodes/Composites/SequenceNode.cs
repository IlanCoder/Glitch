using System;

namespace BehaviorTreeSource.Runtime.Nodes.Composites {
    public class SequenceNode : CompositeNode {
        protected override NodeStatus Tick() {
            while (ChildIndexInListRange()) {
                switch (Children[RunningChildIndex].UpdateNode()) {
                    case NodeStatus.Failed: return NodeStatus.Failed;
                    case NodeStatus.Succeeded:
                        if (WasLastChild()) return NodeStatus.Succeeded;
                        RunningChildIndex++;
                        continue;
                    case NodeStatus.Running: return NodeStatus.Running;
                }
            }
            throw new ArgumentOutOfRangeException(NodeName);
        }
    }
}