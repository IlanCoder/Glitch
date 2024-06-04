using System.Collections.Generic;

namespace BehaviorTreeSource.Runtime.Nodes.Leaves {
    public abstract class LeafNode : BasicNode{
        public override void AddChild(BasicNode newChild) {
            return;
        }

        public override void RemoveChild(BasicNode childToRemove) {
            return;
        }

        public override List<BasicNode> GetChildren() {
            return new List<BasicNode>();
        }
    }
}