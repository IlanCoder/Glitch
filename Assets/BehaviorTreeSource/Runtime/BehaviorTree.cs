﻿using System;
using System.Collections.Generic;
using System.Text;
using BehaviorTreeSource.Runtime.Nodes;
using Characters.NPC;
using UnityEditor;
using UnityEngine;

namespace BehaviorTreeSource.Runtime {
    [CreateAssetMenu(fileName = "BehaviorTree", menuName = "Behavior Tree")]
    public class BehaviorTree : ScriptableObject {
        public BasicNode rootNode;
        NodeStatus treeStatus = NodeStatus.Running;

        public List<BasicNode> Nodes = new List<BasicNode>();

        public BehaviorTreeBlackboard Blackboard = new BehaviorTreeBlackboard();

        public NodeStatus UpdateTree() {
            if (rootNode is null) throw new ArgumentNullException();
            if (treeStatus != NodeStatus.Running) return treeStatus;
            treeStatus = rootNode.UpdateNode();
            return treeStatus;
        }

        void Traverse(BasicNode node, Action<BasicNode> visitor) {
            if (!node) return;
            visitor.Invoke(node);
            List<BasicNode> children = node.GetChildren();
            foreach (BasicNode child in children) {
                Traverse(child, visitor);
            }
        }
        
        public BehaviorTree Clone() {
            BehaviorTree tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone();
            tree.Nodes = new List<BasicNode>();
            Traverse(tree.rootNode, (node)=> tree.Nodes.Add(node));
            return tree;
        }

        public void BindNodesToTree(NpcManager character) {
            Traverse(rootNode, node => node.BindToTree(character, Blackboard));
        }

    #if UNITY_EDITOR
        public BasicNode CreateNode(Type type, Vector2 pos) {
            BasicNode node = ScriptableObject.CreateInstance(type) as BasicNode;
            node.NodeName = CleanNodeName(type);
            node.name = node.NodeName;
            node.GraphPos = pos;
            node.GuId = GUID.Generate().ToString();
            
            Undo.RecordObject(this, "BehaviorTree (Create Node)");
            Nodes.Add(node);

            if (!Application.isPlaying) {
                AssetDatabase.AddObjectToAsset(node, this);
            }
            Undo.RegisterCreatedObjectUndo(node, "BehaviorTree (Create Node)");
            AssetDatabase.SaveAssets();
            return node;
        }

        static string CleanNodeName(Type type) {
            string tempName = type.Name;
            StringBuilder sBuilder = new StringBuilder(tempName);
            sBuilder.Remove(sBuilder.Length - 4, 4);
            for (int i = tempName.Length - 1; i > 0; i--) {
                if (!char.IsUpper(tempName[i])) continue;
                if (char.IsUpper(tempName[i - 1])) continue;
                sBuilder.Insert(i, ' ');
            }
            return sBuilder.ToString();
        }

        public void DeleteNode(BasicNode node) {
            Undo.RecordObject(this, "BehaviorTree (Delete Node)");
            Nodes.Remove(node);
            foreach (BasicNode basicNode in Nodes) {
                if (!basicNode.GetChildren().Contains(node)) continue;
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
    }
}