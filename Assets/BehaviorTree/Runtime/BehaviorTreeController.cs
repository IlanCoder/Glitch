using BehaviorTree.Runtime.Nodes.Composites;
using BehaviorTree.Runtime.Nodes.Decorators;
using BehaviorTree.Runtime.Nodes.Leaves;
using UnityEngine;

namespace BehaviorTree.Runtime {
    public class BehaviorTreeController : MonoBehaviour {
        [SerializeField] BehaviorTree tree;

        void Start() {
            tree = ScriptableObject.CreateInstance<BehaviorTree>();
            RepeaterNode repeaterNode = new RepeaterNode();
            
            SequenceNode sequenceNode = new SequenceNode();
            repeaterNode.SetChild(sequenceNode);
            
            DebugNode debugNode = new DebugNode();
            debugNode.Message="Hi";
            sequenceNode.AddChild(debugNode);

            WaitNode waitNode = new WaitNode();
            waitNode.Duration = 1;
            sequenceNode.AddChild(waitNode);
            
            DebugNode debugNode2 = new DebugNode();
            debugNode2.Message="Hi2";
            sequenceNode.AddChild(debugNode2);
            
            tree.rootNode = repeaterNode;
        }

        void FixedUpdate() {
            tree.UpdateTree();
        }
    }
}