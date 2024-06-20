using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;

namespace AiBehaviorNodes.Movement{
	public class CalculatePathToTargetNode : LeafNode {
		protected override void InitializeNode() {
			NpcAgent.movementController.EnableNavMeshAgent(false);
		}

		protected override NodeStatus Tick() {
			NpcAgent.movementController.CalculatePath(TreeBlackboard.targetCharacter);
			return NpcAgent.movementController.IsPathComplete() ? NodeStatus.Succeeded : NodeStatus.Failed;
		}

		protected override void ExitNode() {
			
		}
	}
}
