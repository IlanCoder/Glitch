using System.Collections.Generic;
using Characters.NPC;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace BehaviorTreeSource.Runtime.Nodes {
    public abstract class BasicNode : ScriptableObject{
        public string NodeName;
        [TextArea] public string Description;
        public NodeStatus Status { get; protected set; }
        public bool Initialized { get; protected set; }
        protected NpcManager NpcAgent;
        protected BehaviorTreeBlackboard TreeBlackboard;

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

        public virtual void ExitNodeEarly() {
            if(Status == NodeStatus.Running) Status = NodeStatus.Failed;
            ExitNode();
            Initialized = false;
        }

        public void BindToTree(NpcManager character, BehaviorTreeBlackboard blackboard) {
            NpcAgent = character;
            TreeBlackboard = blackboard;
        }

        protected abstract void InitializeNode();

        protected abstract NodeStatus Tick();

        protected abstract void ExitNode();

        public abstract void AddChild(BasicNode newChild);

        public abstract void RemoveChild(BasicNode childToRemove);

        public abstract List<BasicNode> GetChildren();
    }
}