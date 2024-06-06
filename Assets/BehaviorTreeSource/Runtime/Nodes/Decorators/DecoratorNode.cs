using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeSource.Runtime.Nodes.Decorators {
    public abstract class DecoratorNode : BasicNode {
        [SerializeField]protected BasicNode Child;

        public override void AddChild(BasicNode newChild) {
            if(newChild == this) return;
            SetChild(newChild);
        }
        public override void RemoveChild(BasicNode childToRemove) {
            if (Child != childToRemove) return;
            Child = null;
        }
        public override List<BasicNode> GetChildren() {
            return !Child ? new List<BasicNode>() : new List<BasicNode> { Child };
        }

        public virtual void SetChild(BasicNode node) => Child = node;
    }
}