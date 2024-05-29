using System;
using BehaviorTreeSource.Runtime;
using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Composites;
using BehaviorTreeSource.Runtime.Nodes.Decorators;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

namespace BehaviorTreeSource.Editor {
    public class BehaviorTreeView : GraphView {
        public new class UxmlFactory : UxmlFactory<BehaviorTreeView, UxmlTraits> { }

        BehaviorTree _tree;
        
        public BehaviorTreeView() {
            Insert(0, new GridBackground());
            
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            StyleSheet styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BehaviorTreeSource/Editor/BehaviorTreeEditor.uss");
            styleSheets.Add(styleSheet);
        }

        internal void PopulateView(BehaviorTree tree) {
            _tree = tree;

            graphViewChanged -= OnGraphviewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphviewChanged;
            
            foreach (BasicNode node in tree.Nodes) {
                CreateNodeView(node);
            }
        }
        
        void CreateNodeView(BasicNode node) {
            NodeView nodeView = new NodeView(node);
            AddElement(nodeView);
        }

        void CreateNode(Type type) {
            BasicNode node = _tree.CreateNode(type);
            CreateNodeView(node);
        }
        
        GraphViewChange OnGraphviewChanged(GraphViewChange graphViewChange) {
            if (graphViewChange.elementsToRemove == null) return graphViewChange;
            foreach (var graphElement in graphViewChange.elementsToRemove) {
                NodeView nodeView = graphElement as NodeView;
                if (nodeView == null) continue;
                _tree.DeleteNode(nodeView.Node);
            }
            return graphViewChange;
        }
        
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt) {
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom<LeafNode>();
            foreach (Type type in types) {
                evt.menu.AppendAction($"Leaf Nodes/{type.Name}", (a) => CreateNode(type));
            }
            types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (Type type in types) {
                evt.menu.AppendAction($"Composite Nodes/{type.Name}", (a) => CreateNode(type));
            }
            types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (Type type in types) {
                evt.menu.AppendAction($"Decorator Nodes/{type.Name}", (a) => CreateNode(type));
            }
        }
    }
}