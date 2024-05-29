using System;
using BehaviorTree.Runtime.Nodes;
using UnityEngine;

namespace BehaviorTree.Runtime {
    [CreateAssetMenu(fileName = "BehaviorTree", menuName = "Behavior Tree")]
    public class BehaviorTree : ScriptableObject {
        public BasicNode rootNode;
        NodeStatus treeStatus = NodeStatus.Running;

        public NodeStatus UpdateTree() {
            if (rootNode == null) throw new ArgumentNullException();
            if (treeStatus != NodeStatus.Running) return treeStatus;
            treeStatus = rootNode.UpdateNode();
            return treeStatus;
        }
    }
}