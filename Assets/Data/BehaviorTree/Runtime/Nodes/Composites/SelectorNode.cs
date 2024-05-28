using System;

namespace Data.BehaviorTree.Runtime.Nodes.Composites {
    public class SelectorNode : CompositeNode {
        public override NodeStatus Tick() {
            while (ChildIndexInListRange()) {
                switch (Children[RunningChildIndex].Tick()) {
                    case NodeStatus.Failed:
                        if (WasLastChild()) return NodeStatus.Failed;
                        RunningChildIndex++;
                        continue;
                    case NodeStatus.Succeeded: return NodeStatus.Succeeded;
                    case NodeStatus.Running: return NodeStatus.Running;
                }
            }
            throw new ArgumentOutOfRangeException(Name);
        }
    }
}