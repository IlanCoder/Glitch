using System;
using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Composites;
using BehaviorTreeSource.Runtime.Nodes.Decorators;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
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
        //inputPort.style.flexDirection = FlexDirection.Column;
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
        //outputPort.style.flexDirection = FlexDirection.ColumnReverse;
        outputContainer.Add(outputPort);
    }


    public override void SetPosition(Rect newPos) {
        base.SetPosition(newPos);
        Node.GraphPos.x = newPos.xMin;
        Node.GraphPos.y = newPos.yMin;
    }

    public override void OnSelected() {
        base.OnSelected();
        OnNodeSelected?.Invoke(this);
    }
}
