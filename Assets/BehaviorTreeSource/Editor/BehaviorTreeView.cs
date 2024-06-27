using System;
using System.Collections.Generic;
using BehaviorTreeSource.Runtime;
using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Composites;
using BehaviorTreeSource.Runtime.Nodes.Decorators;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using Edge = UnityEditor.Experimental.GraphView.Edge;

namespace BehaviorTreeSource.Editor {
    public class BehaviorTreeView : GraphView {
        public new class UxmlFactory : UxmlFactory<BehaviorTreeView, UxmlTraits> { }

        BehaviorTree _tree;
        
        public Action<NodeView> OnNodeSelected;
        
        public BehaviorTreeView() {
            Insert(0, new GridBackground());
            
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            StyleSheet styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BehaviorTreeSource/Editor/BehaviorTreeEditor.uss");
            styleSheets.Add(styleSheet);

            Undo.undoRedoPerformed += OnUndoRedo;
        }

        void OnUndoRedo() {
            PopulateView(_tree);
            AssetDatabase.SaveAssets();
        }

        internal void PopulateView(BehaviorTree tree) {
            _tree = tree;

            graphViewChanged -= OnGraphviewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphviewChanged;

            if (!_tree.rootNode) {
                _tree.rootNode = _tree.CreateNode(typeof(RootNode), Vector2.zero) as RootNode;
                EditorUtility.SetDirty(_tree);
                AssetDatabase.SaveAssets();
            }
            
            foreach (BasicNode node in tree.Nodes) {
                CreateNodeView(node);
            }
            foreach (BasicNode node in tree.Nodes) {
                List<BasicNode> children = node.GetChildren();
                foreach (BasicNode child in children) {
                    NodeView parentView = FindNodeView(node);
                    NodeView childView = FindNodeView(child);
                    Edge edge = parentView.outputPort.ConnectTo(childView.inputPort);
                    AddElement(edge);
                }
            }
        }

        NodeView FindNodeView(BasicNode node) {
            return GetNodeByGuid(node.GuId) as NodeView;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
            List<Port> temp = new List<Port>();
            foreach (Port port in ports) {
                if (port.direction==startPort.direction) continue;
                if (port.node == startPort.node) continue;
                temp.Add(port);
            }
            return temp;
        }

        void CreateNodeView(BasicNode node) {
            NodeView nodeView = new NodeView(node);
            nodeView.OnNodeSelected = OnNodeSelected;
            AddElement(nodeView);
        }

        void CreateNode(Type type, Vector2 mousePos) {
            BasicNode node = _tree.CreateNode(type, mousePos);
            CreateNodeView(node);
        }
        
        GraphViewChange OnGraphviewChanged(GraphViewChange graphViewChange) {
            RemoveTreeNode(graphViewChange);
            CreateTreeEdge(graphViewChange);
            RemoveTreeEdge(graphViewChange);
            CheckMovedNodes(graphViewChange);
            return graphViewChange;
        }

        void CreateTreeEdge(GraphViewChange graphViewChange) {
            if (graphViewChange.edgesToCreate == null) return;
            foreach (Edge edge in graphViewChange.edgesToCreate) {
                NodeView parentNode = edge.output.node as NodeView;
                NodeView childNode = edge.input.node as NodeView;
                if (childNode == null || parentNode == null) return;
                _tree.AddChildToNode(parentNode.Node, childNode.Node);
            }
        }

        void RemoveTreeEdge(GraphViewChange graphViewChange) {
            if (graphViewChange.elementsToRemove == null) return;
            foreach (var graphElement in graphViewChange.elementsToRemove) {
                if (graphElement is not Edge edge) continue;
                NodeView parentNode = edge.output.node as NodeView;
                NodeView childNode = edge.input.node as NodeView;
                if (childNode == null || parentNode == null) return;
                _tree.RemoveChildFromNode(parentNode.Node, childNode.Node);
            }
        }

        void RemoveTreeNode(GraphViewChange graphViewChange) {
            if (graphViewChange.elementsToRemove == null) return;
            foreach (var graphElement in graphViewChange.elementsToRemove) {
                if (graphElement is not NodeView nodeView) continue;
                _tree.DeleteNode(nodeView.Node);
            }
        }

        void CheckMovedNodes(GraphViewChange graphViewChange) {
            if (graphViewChange.movedElements == null) return;
            foreach (Node node in nodes) {
                NodeView nodeView = node as NodeView;
                nodeView?.SortChildren();
            }
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt) {
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom<LeafNode>();
            Vector2 mousePos = evt.localMousePosition;
            mousePos = viewTransform.matrix.inverse.MultiplyPoint(mousePos);
            foreach (Type type in types) {
                evt.menu.AppendAction($"Leaf Nodes/{type.Namespace().Name}/{type.Name}",
                (a) => CreateNode(type, mousePos));
            }
            types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (Type type in types) {
                evt.menu.AppendAction($"Composite Nodes/{type.Name}", (a) => CreateNode(type, mousePos));
            }
            types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (Type type in types) {
                evt.menu.AppendAction($"Decorator Nodes/{type.Name}", (a) => CreateNode(type, mousePos));
            }
        }

        public void UpdateNodeStates() {
            foreach (Node node in nodes) {
                NodeView nodeView = node as NodeView;
                nodeView?.UpdateState();
            }
        }
    }
}