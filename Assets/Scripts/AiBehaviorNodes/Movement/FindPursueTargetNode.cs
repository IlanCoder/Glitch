using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using Characters;

namespace AiBehaviorNodes.Movement {
    public class FindPursueTargetNode : LeafNode {
        CharacterManager _target;
        
        protected override void InitializeNode() {
            _target = null;
        }

        protected override NodeStatus Tick() {
            return !NpcAgent.agroController.CheckLineSightRadius(out _target) ? NodeStatus.Failed : NodeStatus.Succeeded;
        }

        protected override void ExitNode() {
            TreeBlackboard.targetCharacter = _target;
        }
    }
}