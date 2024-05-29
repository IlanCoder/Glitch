using UnityEngine.UIElements;

namespace BehaviorTreeSource.Editor {
    public class SplitView : TwoPaneSplitView {
        public new class UxmlFactory : UxmlFactory<SplitView, UxmlTraits> { }

        public SplitView() {
            
        }
    }
}
