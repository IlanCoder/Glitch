using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviorTreeSource.Runtime.Nodes.Composites {
    public abstract class CompositeNode : BasicNode { 
        [SerializeField]protected List<BasicNode> Children = new List<BasicNode>();
        protected ushort RunningChildIndex = 0;
        
        override public BasicNode Clone() {
            CompositeNode node = Instantiate(this);
            node.Children = Children.ConvertAll(child => child.Clone());
            return node;
        }
        
        protected override void InitializeNode() {
            ResetChildIndex();
        }
        
        protected override void ExitNode() { }

        public override void ExitNodeEarly() {
            if(Children[RunningChildIndex].Status == NodeStatus.Running) Children[RunningChildIndex].ExitNodeEarly();
            base.ExitNodeEarly();
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
        
        public override void AddChild(BasicNode newChild) {
            if(newChild == this) return;
            if(Children.Contains(newChild)) return;
            AddChildToList(newChild);
        }

        public override void RemoveChild(BasicNode childToRemove) {
            if(childToRemove == this) return;
            if(!Children.Contains(childToRemove)) return;
            RemoveChildFromList(childToRemove);
        }
        
        public override List<BasicNode> GetChildren() {
            return Children;
        }

        public void AddChildToList(BasicNode node) => Children.Add(node);

        public void RemoveChildFromList(BasicNode node) => Children.Remove(node);

        public void SortChildrenByComparison(Comparison<BasicNode> comparison) {
            Children.Sort(comparison);
        }
    }
}