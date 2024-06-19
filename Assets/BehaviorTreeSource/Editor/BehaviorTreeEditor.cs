using System;
using BehaviorTreeSource.Runtime;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace BehaviorTreeSource.Editor {
    public class BehaviorTreeEditor : EditorWindow {
        BehaviorTreeView _treeView;
        InspectorView _inspectorView;
        IMGUIContainer _blackboardView;

        SerializedObject _treeObject;
        SerializedProperty _blackboardProperty;
        
        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        [MenuItem("BehaviorTreeEditor/Editor")]
        public static void OpenWindow() {
            BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
            wnd.titleContent = new GUIContent("BehaviorTreeEditor");
        }

        [OnOpenAsset]
        public static bool OnOpenWindow(int instanceId, int line) {
            if (Selection.activeObject is not BehaviorTree) return false;
            OpenWindow();
            return true;
        }

        void OnEnable() {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        void OnDisable() {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        void OnPlayModeStateChanged(PlayModeStateChange obj) {
            switch (obj) {
                case PlayModeStateChange.EnteredEditMode:
                    OnSelectionChange();
                    break;
                case PlayModeStateChange.ExitingEditMode: break;
                case PlayModeStateChange.EnteredPlayMode:
                    OnSelectionChange();
                    break;
                case PlayModeStateChange.ExitingPlayMode: break;
                default: throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
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
            _blackboardView = root.Q<IMGUIContainer>();
            _blackboardView.onGUIHandler = () => {
                _treeObject?.Update();
                if (_blackboardProperty == null) return;
                EditorGUILayout.PropertyField(_blackboardProperty);
            };

            _treeView.OnNodeSelected += OnNodeSelectionChanged;
            OnSelectionChange();
        }

        public void OnSelectionChange() {
            BehaviorTree tree = Selection.activeObject as BehaviorTree;
            if (!tree) {
                if (!Selection.activeGameObject) return;
                if (!Selection.activeGameObject.TryGetComponent(out BehaviorTreeController treeController)) return;
                tree = treeController.Tree;
            }

            if (!Application.isPlaying && !AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID())) return;
            _treeView?.PopulateView(tree);

            if (!tree) return;
            _treeObject = new SerializedObject(tree);
            _blackboardProperty = _treeObject.FindProperty("Blackboard");
        }

        void OnNodeSelectionChanged(NodeView node) {
            _inspectorView.UpdateSelection(node);
        }

        void OnInspectorUpdate() {
            _treeView?.UpdateNodeStates();
        }
    }
}
