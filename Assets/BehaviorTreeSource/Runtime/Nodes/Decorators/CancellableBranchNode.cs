using System.Data;
using UnityEngine.Events;

namespace BehaviorTreeSource.Runtime.Nodes.Decorators {
    public class CancellableNode : DecoratorNode {
        public string cancellableTokenName;

        bool _earlyExit;
        bool _nameTaken;

        protected override void InitializeNode() {
            if (TreeBlackboard.CancelEvents.TryAdd(cancellableTokenName, _earlyExit)) return;
            _nameTaken = true;
            throw new DuplicateNameException($"{cancellableTokenName} already exists in the cancel events dictionary");
        }

        protected override NodeStatus Tick() {
            return _earlyExit ? NodeStatus.Failed : Child.UpdateNode();
        }

        protected override void ExitNode() {
            if(!_nameTaken) TreeBlackboard.CancelEvents.Remove(cancellableTokenName);
            _earlyExit = false;
        }
    }
}