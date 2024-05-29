using UnityEngine;

namespace BehaviorTree.Runtime.Nodes.Leaves {
    public class DebugNode : LeafNode {
        public string Message;
        
        protected override void InitializeNode() { }

        protected override void ExitNode() { }
        
        protected override NodeStatus Tick() {
            #if UNITY_EDITOR
            Debug.Log(Message);
            #endif
            return NodeStatus.Succeeded;
        }
    }
}