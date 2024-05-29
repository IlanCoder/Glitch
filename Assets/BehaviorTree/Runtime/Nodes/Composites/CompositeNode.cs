using System.Collections.Generic;

namespace BehaviorTree.Runtime.Nodes.Composites {
    public abstract class CompositeNode : BasicNode {
        protected List<BasicNode> Children = new List<BasicNode>();
        protected ushort RunningChildIndex = 0;

        protected override void InitializeNode() {
            ResetChildIndex();
        }
        protected override void ExitNode() { }

        protected bool ChildIndexInListRange() {
            return RunningChildIndex < Children.Count;
        }
        
        protected bool WasLastChild() {
            return RunningChildIndex >= Children.Count - 1;
        }

        protected void ResetChildIndex() {
            RunningChildIndex = 0;
        }
    }
}