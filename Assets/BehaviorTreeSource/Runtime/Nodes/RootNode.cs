using System.Collections.Generic;

namespace BehaviorTreeSource.Runtime.Nodes {
    public class RootNode : BasicNode {

        protected BasicNode Child;
        
        protected override void InitializeNode() { }

        protected override NodeStatus Tick() {
            return Child.UpdateNode();
        }

        protected override void ExitNode() { }

        override public void AddChild(BasicNode newChild) {
            if(newChild == this) return;
            Child = newChild;
        }

        override public void RemoveChild(BasicNode childToRemove) {
            if (Child != childToRemove) return;
            Child = null;
        }

        override public List<BasicNode> GetChildren() {
            return !Child ? new List<BasicNode>() : new List<BasicNode> { Child };
        }
    }
}