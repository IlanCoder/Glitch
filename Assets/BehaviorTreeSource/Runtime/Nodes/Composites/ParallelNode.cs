namespace BehaviorTreeSource.Runtime.Nodes.Composites {
    public class ParallelNode : CompositeNode {
        protected NodeStatus ReturnStatus;
        protected bool EarlyExit;
        
        protected override NodeStatus Tick() {
            ReturnStatus = NodeStatus.Succeeded;
            EarlyExit = true;
            foreach (BasicNode basicNode in Children) {
                switch (basicNode.UpdateNode()) {
                    case NodeStatus.Failed: return NodeStatus.Failed;
                    case NodeStatus.Running: 
                        ReturnStatus = NodeStatus.Running;
                        break;
                }
            }
            EarlyExit = false;
            return ReturnStatus;
        }

        protected override void ExitNode() {
            base.ExitNode();
            if (!EarlyExit) return;
            EarlyExit = false;
            foreach (BasicNode child in Children) {
                if(child.Status != NodeStatus.Running) continue;
                child.ExitNodeEarly();
            }
        }

        public override void ExitNodeEarly() {
            foreach (BasicNode child in Children) {
                if(child.Status != NodeStatus.Running) continue;
                child.ExitNodeEarly();
            }
            base.ExitNodeEarly();
        }
    }
}