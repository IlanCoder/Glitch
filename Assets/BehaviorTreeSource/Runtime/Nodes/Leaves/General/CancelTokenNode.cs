using System;
using System.Collections.Generic;

namespace BehaviorTreeSource.Runtime.Nodes.Leaves.General {
    public class CancelTokenNode : LeafNode {
        public string cancellableTokenName;

        bool _keyFound = true;
        
        protected override void InitializeNode() {
            if (TreeBlackboard.CancelEvents.ContainsKey(cancellableTokenName)) return;
            _keyFound = false;
            throw new KeyNotFoundException();
        }

        protected override NodeStatus Tick() {
            if (!_keyFound) return NodeStatus.Failed;
            TreeBlackboard.CancelEvents[cancellableTokenName]?.Invoke();
            return NodeStatus.Succeeded;
        }

        protected override void ExitNode() {
            _keyFound = true;
        }
        
        #if UNITY_EDITOR
        protected void OnValidate() {
            Description = cancellableTokenName;
        }
#endif
    }
}