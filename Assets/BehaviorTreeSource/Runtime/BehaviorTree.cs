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

        public List<BasicNode> Nodes = new List<BasicNode>();

        public NodeStatus UpdateTree() {
            if (rootNode is null) throw new ArgumentNullException();
            if (treeStatus != NodeStatus.Running) return treeStatus;
            treeStatus = rootNode.UpdateNode();
            return treeStatus;
        }

    #if UNITY_EDITOR
        public BasicNode CreateNode(Type type) {
            BasicNode node = ScriptableObject.CreateInstance(type) as BasicNode;
            node.NodeName = type.Name;
            node.name = node.NodeName;
            node.GuId = GUID.Generate().ToString();
            
            Undo.RecordObject(this, "BehaviorTree (Create Node)");
            Nodes.Add(node);
            
            AssetDatabase.AddObjectToAsset(node, this);
            Undo.RegisterCreatedObjectUndo(node, "BehaviorTree (Create Node)");
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(BasicNode node) {
            Undo.RecordObject(this, "BehaviorTree (Delete Node)");
            Nodes.Remove(node);
            foreach (BasicNode basicNode in Nodes) {
                if (!GetNodeChildren(basicNode).Contains(node)) continue;
                RemoveChildFromNode(basicNode, node);
            }
            //AssetDatabase.RemoveObjectFromAsset(node);
            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChildToNode(BasicNode parent, BasicNode child) {
            Undo.RecordObject(parent, "BehaviorTree (Add Child)");
            parent.AddChild(child);
            AssetDatabase.SaveAssets();
        }
        
        public void RemoveChildFromNode(BasicNode parent, BasicNode child) {
            if(!parent) return;
            Undo.RecordObject(parent, "BehaviorTree (Remove Child)");
            parent.RemoveChild(child);
            AssetDatabase.SaveAssets();
        }
    #endif

        public List<BasicNode> GetNodeChildren(BasicNode parent) {
            return parent.GetChildren();
        }
    }
}