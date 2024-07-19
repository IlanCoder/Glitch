using System.Data;

namespace BehaviorTreeSource.Runtime.Nodes.Decorators {
    public class CancellableRepeaterNode : RepeaterNode{
        public string cancellableTokenName;

        bool _earlyExit;
        bool _nameTaken;
        
        protected override void InitializeNode() {
            base.InitializeNode();
            if (TreeBlackboard.CancelEvents.TryAdd(cancellableTokenName, TriggerEarlyExit)) return;
            _nameTaken = true;
            throw new DuplicateNameException($"{cancellableTokenName} already exists in the cancel events dictionary");
        }
        
        protected override NodeStatus Tick() {
            return _earlyExit ? NodeStatus.Failed : base.Tick();
        }
        
        protected override void ExitNode() {
            base.ExitNode();
            if (!_nameTaken) TreeBlackboard.CancelEvents.Remove(cancellableTokenName);
            if (!_earlyExit) return;
            _earlyExit = false;
            if (Child.Status == NodeStatus.Running) Child.ExitNodeEarly();
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