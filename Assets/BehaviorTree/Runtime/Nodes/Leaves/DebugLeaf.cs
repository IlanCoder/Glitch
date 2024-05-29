using Data.BehaviorTree.Runtime.Nodes.Leaves;
using UnityEngine;

namespace BehaviorTree.Runtime.Nodes.Leaves {
    public class DebugLeaf : LeafNode {
        protected override void InitializeNode() { }

        protected override void ExitNode() { }
        
        protected override NodeStatus Tick() {
            #if UNITY_EDITOR
            Debug.Log($"{Name} Ticked");
            #endif
            return NodeStatus.Succeeded;
        }
    }
}