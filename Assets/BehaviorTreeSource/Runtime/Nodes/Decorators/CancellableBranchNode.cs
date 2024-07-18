using System.Data;
using UnityEngine.Events;

namespace BehaviorTreeSource.Runtime.Nodes.Decorators {
    public class CancellableBranchNode : DecoratorNode {
        public string cancellableTokenName;

        bool _earlyExit;
        bool _nameTaken;

        protected override void InitializeNode() {
            if (TreeBlackboard.CancelEvents.TryAdd(cancellableTokenName, TriggerEarlyExit)) return;
            _nameTaken = true;
            throw new DuplicateNameException($"{cancellableTokenName} already exists in the cancel events dictionary");
        }

        protected override NodeStatus Tick() {
            if (!_earlyExit) return Child.UpdateNode();
            if (Child.Status != NodeStatus.Running) return NodeStatus.Failed;
            Child.ExitNodeEarly();
            return NodeStatus.Failed;
        }

        protected override void ExitNode() {
            if(!_nameTaken) TreeBlackboard.CancelEvents.Remove(cancellableTokenName);
            _earlyExit = false;
        }
        
        protected void TriggerEarlyExit() {
            _earlyExit = true;
        }
        
        #if UNITY_EDITOR
        protected void OnValidate() {
            Description = cancellableTokenName + " Branch";
        }
        #endif
    }
}