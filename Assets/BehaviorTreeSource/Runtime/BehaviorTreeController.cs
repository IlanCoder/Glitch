using BehaviorTreeSource.Runtime.Nodes.Composites;
using BehaviorTreeSource.Runtime.Nodes.Decorators;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using UnityEngine;

namespace BehaviorTreeSource.Runtime {
    public class BehaviorTreeController : MonoBehaviour {
        [SerializeField] BehaviorTree tree;

        void Start() {
            tree = Instantiate(tree);
        }

        void FixedUpdate() {
            tree.UpdateTree();
        }
    }
}