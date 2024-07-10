using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using Characters;

namespace AiBehaviorNodes.Movement {
    public class IdleUntilAgroNode : LeafNode {
        CharacterManager _target;
        protected override void InitializeNode() {
            _target = null;
        }

        protected override NodeStatus Tick() {
            try {
                NpcAgent.movementController.GoIdle();
                return NpcAgent.agroController.CheckLineSightRadius(out _target)
                    ? NodeStatus.Succeeded
                    : NodeStatus.Running;
            }
            catch {
                return NodeStatus.Failed;
            }
        }

        protected override void ExitNode() {
            TreeBlackboard.targetCharacter = _target;
        }
    }
}