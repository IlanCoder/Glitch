using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeSource.Runtime.Nodes {

    public abstract class BasicNode : ScriptableObject{
        public string NodeName;
        public NodeStatus Status { get; protected set; }
        public bool Initialized { get; protected set; }

        #if UNITY_EDITOR
        [HideInInspector] public string GuId;
        [HideInInspector] public Vector2 GraphPos;
        #endif

        void Awake() {
            Initialized = false;
        }

        public virtual BasicNode Clone() {
            return Instantiate(this);
        }

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

        public abstract void AddChild(BasicNode newChild);

        public abstract void RemoveChild(BasicNode childToRemove);

        public abstract List<BasicNode> GetChildren();
    }
}