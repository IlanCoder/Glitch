using BehaviorTreeSource.Runtime.Nodes.Composites;
using BehaviorTreeSource.Runtime.Nodes.Decorators;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using UnityEngine;

namespace BehaviorTreeSource.Runtime {
    public class BehaviorTreeController : MonoBehaviour {
        [SerializeField] BehaviorTree tree;
        public BehaviorTree Tree => tree;

        void Start() {
            tree = tree.Clone();
        }

        void FixedUpdate() {
            tree.UpdateTree();
        }
    }
}