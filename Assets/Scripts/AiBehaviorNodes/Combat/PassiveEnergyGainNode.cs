using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using UnityEngine;

namespace AiBehaviorNodes.Combat {
    public class PassiveEnergyGainNode : LeafNode {
        protected override void InitializeNode() {
        }

        protected override NodeStatus Tick() {
            NpcAgent.statsController.GainAgroEnergy(Time.fixedDeltaTime);
            return NodeStatus.Running;
        }

        protected override void ExitNode() {
        }
    }
}