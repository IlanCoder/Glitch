using UnityEngine;

namespace BehaviorTree.Runtime.Nodes.Composites {
    public class RandomSequenceNode : SequenceNode{
        protected override void InitializeNode() {
            ShuffleChildren();
            base.InitializeNode();
        }

        protected void ShuffleChildren() {
            if (Children.Count <= 0) return;
            for (int i = 0; i < Children.Count; i++) {
                int swapTarget = Random.Range(0, Children.Count);
                (Children[i], Children[swapTarget]) = (Children[swapTarget], Children[i]);
            }
        }
    }
}