using System;
using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Composites;
using BehaviorTreeSource.Runtime.Nodes.Decorators;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class NodeView : Node {
    public Action<NodeView> OnNodeSelected;
    public BasicNode Node { get; private set; }
    public Port inputPort;
    public Port outputPort;
    
    public NodeView(BasicNode node) : base("Assets/BehaviorTreeSource/Editor/Nodes/NodeView.uxml") {
        Node = node;
        title = node.NodeName;
        viewDataKey = node.GuId;
        
        style.left = node.GraphPos.x;
        style.top = node.GraphPos.y;

        CreateInputPorts();
        CreateOutputPorts();
        SetupClasses();

        Label descriptionLabel = this.Q<Label>("description");
        descriptionLabel.bindingPath = "Description";
        descriptionLabel.Bind(new SerializedObject(node));
    }

    void SetupClasses() {
        switch (Node) {
            case CompositeNode:
                AddToClassList("Composite");
                break;
            case DecoratorNode:
                AddToClassList("Decorator");
                break;
            case LeafNode:
                AddToClassList("Leaf");
                break;
            case RootNode: 
                AddToClassList("Root");
                return;
        }
    }

    void CreateInputPorts() {
        switch (Node) {
            case CompositeNode:
                inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case DecoratorNode: 
                inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case LeafNode: 
                inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case RootNode: return;
        }
        if (inputPort == null) return;
        inputPort.portName = "";
        inputPort.style.flexDirection = FlexDirection.Column;
        inputContainer.Add(inputPort);
    }
    
    void CreateOutputPorts() {
        switch (Node) {
            case CompositeNode: 
                outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
                break;
            case DecoratorNode: 
                outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
            case LeafNode: return;
            case RootNode:
                outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
        }
        if (outputPort == null) return;
        outputPort.portName = "";
        outputPort.style.flexDirection = FlexDirection.ColumnReverse;
        outputContainer.Add(outputPort);
    }

    public override void SetPosition(Rect newPos) {
        base.SetPosition(newPos);
        Undo.RecordObject(Node,"BehaviorTree (Set Position)");
        Node.GraphPos.x = newPos.xMin;
        Node.GraphPos.y = newPos.yMin;
        EditorUtility.SetDirty(Node);
    }

    public override void OnSelected() {
        base.OnSelected();
        OnNodeSelected?.Invoke(this);
    }

    public void SortChildren() {
        CompositeNode tempNode= Node as CompositeNode;
        if (!tempNode) return;
        tempNode.SortChildrenByComparison(SortByHorizontalPosition);
    }

    int SortByHorizontalPosition(BasicNode left, BasicNode right) {
        return left.GraphPos.x < right.GraphPos.x ? -1 : 1;
    }

    public void UpdateState() {
        RemoveFromClassList("FailedNode");
        RemoveFromClassList("SucceededNode");
        RemoveFromClassList("RunningNode");
        if (!Application.isPlaying) return;
        switch (Node.Status) {
            case NodeStatus.Failed:
                AddToClassList("FailedNode");
                break;
            case NodeStatus.Succeeded: 
                AddToClassList("SucceededNode");
                break;
            case NodeStatus.Running: 
                if(!Node.Initialized) return;
                AddToClassList("RunningNode");
                break;
        }
    }
}
