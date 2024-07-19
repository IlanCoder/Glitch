namespace BehaviorTreeSource.Runtime.Nodes.Composites {
    public class ParallelNode : CompositeNode {
        protected NodeStatus Status;
        protected bool EarlyExit;
        
        protected override NodeStatus Tick() {
            Status = NodeStatus.Succeeded;
            EarlyExit = true;
            foreach (BasicNode basicNode in Children) {
                switch (basicNode.UpdateNode()) {
                    case NodeStatus.Failed: return NodeStatus.Failed;
                    case NodeStatus.Running: 
                        Status = NodeStatus.Running;
                        break;
                }
            }
            EarlyExit = false;
            return Status;
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