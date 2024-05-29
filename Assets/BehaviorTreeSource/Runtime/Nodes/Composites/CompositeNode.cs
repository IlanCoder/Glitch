using System.Collections.Generic;

namespace BehaviorTreeSource.Runtime.Nodes.Composites {
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

        public void AddChild(BasicNode node) => Children.Add(node);

        public void RemoveChild(BasicNode node) => Children.Remove(node);
    }
}