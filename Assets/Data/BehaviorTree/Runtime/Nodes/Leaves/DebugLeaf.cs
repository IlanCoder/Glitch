using UnityEngine;

namespace Data.BehaviorTree.Runtime.Nodes.Leaves {
    public class DebugLeaf : LeafNode {
        public override NodeStatus Tick() {
            #if UNITY_EDITOR
            Debug.Log($"{Name} Ticked");
            #endif
            return NodeStatus.Succeeded;
        }
    }
}