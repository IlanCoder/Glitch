using System;
using System.Collections.Generic;
using BehaviorTreeSource.Runtime.Nodes;
using UnityEditor;
using UnityEngine;

namespace BehaviorTreeSource.Runtime {
    [CreateAssetMenu(fileName = "BehaviorTree", menuName = "Behavior Tree")]
    public class BehaviorTree : ScriptableObject {
        public BasicNode rootNode;
        NodeStatus treeStatus = NodeStatus.Running;
        public List<BasicNode> Nodes { get; protected set; } = new List<BasicNode>();

        public NodeStatus UpdateTree() {
            if (rootNode == null) throw new ArgumentNullException();
            if (treeStatus != NodeStatus.Running) return treeStatus;
            treeStatus = rootNode.UpdateNode();
            return treeStatus;
        }

        public BasicNode CreateNode(Type type) {
            BasicNode node = Activator.CreateInstance(type) as BasicNode;
            node.NodeName = type.Name;
            
            #if UNITY_EDITOR
            node.GuId = GUID.Generate().ToString();
            #endif
            
            Nodes.Add(node);
            return node;
        }

        public void DeleteNode(BasicNode node) {
            Nodes.Remove(node);
        }
    }
}