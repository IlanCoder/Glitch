using System.Collections.Generic;

namespace Data.BehaviorTree.Runtime.Nodes.Composites {
    public abstract class CompositeNode : BasicNode {
        protected List<BasicNode> Children = new List<BasicNode>();
        protected ushort RunningChildIndex = 0;

        public override void Initialize() {
            RunningChildIndex = 0;
            base.Initialize();
        }

        public override void Reset() {
            foreach (BasicNode child in Children) {
                child.Reset();
            }
            ResetChildIndex();
            base.Reset();
        }

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