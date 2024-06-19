using BehaviorTreeSource.Runtime.Nodes.Composites;
using BehaviorTreeSource.Runtime.Nodes.Decorators;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using Characters.NPC;
using UnityEngine;

namespace BehaviorTreeSource.Runtime {
    [RequireComponent(typeof(NpcManager))]
    public class BehaviorTreeController : MonoBehaviour {
        [SerializeField] BehaviorTree tree;
        public BehaviorTree Tree => tree;

        void Start() {
            tree = tree.Clone();
            tree.BindNodesToTree(GetComponent<NpcManager>());
        }

        void FixedUpdate() {
            tree.UpdateTree();
        }
    }
}