using UnityEngine;

namespace Data.BehaviorTree.Runtime.Nodes.Composites {
    public class RandomSelectorNode : SelectorNode {
        public override void Initialize() {
            ShuffleChildren();
            base.Initialize();
        }

        public override void Reset() {
            ShuffleChildren();
            base.Reset();
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