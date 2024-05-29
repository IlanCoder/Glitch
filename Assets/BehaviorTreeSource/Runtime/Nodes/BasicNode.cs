using UnityEngine;

namespace BehaviorTreeSource.Runtime.Nodes {
    public abstract class BasicNode {
        public string NodeName;
        public NodeStatus Status { get; protected set; }
        public bool Initialized { get; protected set; }
        #if UNITY_EDITOR
        public string GuId;
        public Vector2 GraphPos;
        #endif 
        
        public NodeStatus UpdateNode() {
            if (!Initialized) {
                InitializeNode();
                Initialized = true;
            }
            Status = Tick();
            if (Status == NodeStatus.Running) return Status;
            ExitNode();
            Initialized = false;
            return Status;
        }
        
        protected abstract void InitializeNode();
        protected abstract NodeStatus Tick();
        protected abstract void ExitNode();
    }
}