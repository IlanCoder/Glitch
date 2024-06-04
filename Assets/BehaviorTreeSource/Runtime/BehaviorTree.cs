﻿using System;
using System.Collections.Generic;
using BehaviorTreeSource.Runtime.Nodes;
using UnityEditor;
using UnityEngine;

namespace BehaviorTreeSource.Runtime {
    [CreateAssetMenu(fileName = "BehaviorTree", menuName = "Behavior Tree")]
    public class BehaviorTree : ScriptableObject {
        public BasicNode rootNode;
        NodeStatus treeStatus = NodeStatus.Running;

        public List<BasicNode> Nodes = new List<BasicNode>();

        public NodeStatus UpdateTree() {
            if (rootNode is null) throw new ArgumentNullException();
            if (treeStatus != NodeStatus.Running) return treeStatus;
            treeStatus = rootNode.UpdateNode();
            return treeStatus;
        }

        public BasicNode CreateNode(Type type) {
            BasicNode node = ScriptableObject.CreateInstance(type) as BasicNode;
            node.NodeName = type.Name;
            node.name = node.NodeName;
            node.GuId = GUID.Generate().ToString();

            Nodes.Add(node);
            
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            
            return node;
        }

        public void DeleteNode(BasicNode node) {
            Nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChildToNode(BasicNode parent, BasicNode child) {
            parent.AddChild(child);
        }
        
        public void RemoveChildFromNode(BasicNode parent, BasicNode child) {
            parent.RemoveChild(child);
        }

        public List<BasicNode> GetNodeChildren(BasicNode parent) {
            return parent.GetChildren();
        }
    }
}