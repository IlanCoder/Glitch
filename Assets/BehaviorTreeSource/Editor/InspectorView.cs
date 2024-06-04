using UnityEngine.UIElements;

namespace BehaviorTreeSource.Editor {
    public class InspectorView : VisualElement {
        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits> { }

        UnityEditor.Editor _editor;
        
        public InspectorView() {
            
        }

        public void UpdateSelection(NodeView node) {
            Clear();
            UnityEngine.Object.DestroyImmediate(_editor);
            if(node == null) return;
            _editor = UnityEditor.Editor.CreateEditor(node.Node);
            IMGUIContainer container = new IMGUIContainer(() => { _editor.OnInspectorGUI(); });
            Add(container);
        }
    }
}
