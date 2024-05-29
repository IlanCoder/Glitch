using UnityEngine;

namespace BehaviorTreeSource.Runtime.Nodes.Leaves {
    public class WaitNode : LeafNode {
        public float Duration;
        protected float StartTime;
        
        protected override void InitializeNode() {
            StartTime = Time.time;
        }

        protected override NodeStatus Tick() {
            return Time.time - StartTime > Duration ? NodeStatus.Succeeded : NodeStatus.Running;
        }

        protected override void ExitNode() {
            StartTime = 0;
        }
    }
}