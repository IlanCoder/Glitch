using System;

namespace Data.BehaviorTree.Runtime.Nodes.Composites {
    public class SequenceNode : CompositeNode {
        public override NodeStatus Tick() {
            while (ChildIndexInListRange()) {
                switch (Children[RunningChildIndex].Tick()) {
                    case NodeStatus.Failed: return NodeStatus.Failed;
                    case NodeStatus.Succeeded:
                        if (WasLastChild()) return NodeStatus.Succeeded;
                        RunningChildIndex++;
                        continue;
                    case NodeStatus.Running: return NodeStatus.Running;
                }
            }
            throw new ArgumentOutOfRangeException(Name);
        }
    }
}