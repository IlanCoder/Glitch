using BehaviorTreeSource.Runtime.Nodes;
using UnityEngine;

public class NodeView : UnityEditor.Experimental.GraphView.Node {
    public BasicNode Node { get; private set; }
    
    public NodeView(BasicNode node) {
        Node = node;
        title = node.NodeName;
        viewDataKey = node.GuId;
        
        style.left = node.GraphPos.x;
        style.top = node.GraphPos.y;
    }

    public override void SetPosition(Rect newPos) {
        base.SetPosition(newPos);
        Node.GraphPos.x = newPos.xMin;
        Node.GraphPos.y = newPos.yMin;
    }
}
