using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviorTreeSource.Runtime.Nodes {
    public class RootNode : BasicNode {

        [SerializeField]protected BasicNode Child;

        override public BasicNode Clone() {
            RootNode node = Instantiate(this);
            node.AddChild(Child.Clone());
            return node;
        }

        protected override void InitializeNode() { }

        protected override NodeStatus Tick() {
            return NpcAgent.isDead ? NodeStatus.Failed : Child.UpdateNode();
        }

        protected override void ExitNode() {
            if (Child.Status == NodeStatus.Running) Child.ExitNodeEarly();
        }

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