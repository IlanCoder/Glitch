using BehaviorTreeSource.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BehaviorTreeSource.Editor {
    public class BehaviorTreeEditor : EditorWindow {
        BehaviorTreeView _treeView;
        InspectorView _inspectorView;
        
        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        [MenuItem("BehaviorTreeEditor/Editor")]
        public static void OpenWindow() {
            BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
            wnd.titleContent = new GUIContent("BehaviorTreeEditor");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;
            StyleSheet styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BehaviorTreeSource/Editor/BehaviorTreeEditor.uss");
            root.styleSheets.Add(styleSheet);
            m_VisualTreeAsset.CloneTree(root);

            _treeView = root.Q<BehaviorTreeView>();
            _inspectorView = root.Q<InspectorView>();
            
            OnSelectionChange();
        }

        public void OnSelectionChange() {
            BehaviorTree tree = Selection.activeObject as BehaviorTree;
            if (!tree) return;
            _treeView.PopulateView(tree);
        }
    }
}
