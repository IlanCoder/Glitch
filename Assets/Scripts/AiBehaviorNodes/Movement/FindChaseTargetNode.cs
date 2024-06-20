using BehaviorTreeSource.Runtime.Nodes;
using BehaviorTreeSource.Runtime.Nodes.Leaves;
using Characters;

namespace AiBehaviorNodes.Movement {
    public class FindChaseTargetNode : LeafNode {
        CharacterManager _target;
        
        protected override void InitializeNode() {
            _target = null;
        }

        protected override NodeStatus Tick() {
            NpcAgent.combatController.CheckLineSightRadius(out _target);
            return _target is null ? NodeStatus.Failed : NodeStatus.Succeeded;
        }

        protected override void ExitNode() {
            TreeBlackboard.targetCharacter = _target;
        }
    }
}