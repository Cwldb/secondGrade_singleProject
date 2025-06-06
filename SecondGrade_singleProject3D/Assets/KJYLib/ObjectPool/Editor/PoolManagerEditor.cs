using KJYLib.ObjectPool.RunTime;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace KJYLib.ObjectPool.Editor
{
    public class PoolManagerEditor : EditorWindow
    {
        [SerializeField] private VisualTreeAsset visualTreeAsset = default;
        [SerializeField] private VisualTreeAsset itemAsset;
        [SerializeField] private PoolManagerSO poolManager;

        private string _rootFolderPath;
        private Button _createBtn;
        private ScrollView _itemView;
        private List<PoolItemUI> _itemList;
        private PoolItemUI _selectedItem;

        private UnityEditor.Editor _cachedEditor;
        private VisualElement _inspectorView;

        [MenuItem("Tools/PoolManagerEditor")]
        public static void ShowWindow()
        {
            PoolManagerEditor wnd = GetWindow<PoolManagerEditor>();
            wnd.titleContent = new GUIContent("PoolManagerEditor");
            //wnd.minSize = new Vector2(1000, 800);
        }

        public void CreateGUI()
        {
            InitializeWindow();

            VisualElement root = rootVisualElement;

            visualTreeAsset.CloneTree(root);

            SetElements(root);
        }

        private void SetElements(VisualElement root)
        {
            _createBtn = root.Q<Button>("CreateBtn");
            _createBtn.clicked += HandleCreateBtn;
            _itemView = root.Q<ScrollView>("ItemView");

            _itemList = new List<PoolItemUI>();

            _inspectorView = root.Q<VisualElement>("InspectorView");

            GeneratePoolItems();
        }

        private void HandleCreateBtn()
        {
            PoolItemSO newItem = ScriptableObject.CreateInstance<PoolItemSO>();
            Guid itemGuid = Guid.NewGuid();
            newItem.poolingName = itemGuid.ToString();

            if (Directory.Exists($"{_rootFolderPath}/Items") == false)
                Directory.CreateDirectory($"{_rootFolderPath}/Items");

            AssetDatabase.CreateAsset(newItem, $"{_rootFolderPath}/Items/{newItem.poolingName}.asset");

            poolManager.itemList.Add(newItem);
            EditorUtility.SetDirty(poolManager);
            AssetDatabase.SaveAssets();
            // 이걸 안하면 유니티를 끄면 명령어가 사라짐

            GeneratePoolItems();
        }

        private void GeneratePoolItems()
        {
            _itemView.Clear();
            _itemList.Clear();
            _inspectorView.Clear();

            foreach (var item in poolManager.itemList)
            {
                TemplateContainer itemTemplate = itemAsset.Instantiate();
                PoolItemUI itemUI = new PoolItemUI(itemTemplate, item);
                _itemView.Add(itemTemplate);
                _itemList.Add(itemUI);

                itemUI.Name = item.poolingName;

                if (_selectedItem != null && _selectedItem.poolItem == item)
                {
                    HandleItemSelect(itemUI);
                    // 인스펙터 뷰 보여주게 하는 것 처리 해야 함
                }

                itemUI.OnSelectEvent += HandleItemSelect;
                itemUI.OnDeleteEvent += HandleItemDelete;
            }
        }

        private void HandleItemDelete(PoolItemUI target)
        {
            if(EditorUtility.DisplayDialog("Delete Pool Item", $"Are you sure you want to delete {target.Name}?", "Yes", "No") == false)
                return;

            poolManager.itemList.Remove(target.poolItem);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(target.poolItem)); // 실질적인 삭제
            EditorUtility.SetDirty(poolManager); // 더티 플래그 설정
            AssetDatabase.SaveAssets(); // 에셋 저장

            if (_selectedItem == target)
            {
                _selectedItem = null; 
            }

            GeneratePoolItems();
        }

        private void HandleItemSelect(PoolItemUI target)
        {
            _inspectorView.Clear();
            if (_selectedItem != null)
                _selectedItem.IsActive = false;
            _selectedItem = target;
            _selectedItem.IsActive = true;

            UnityEditor.Editor.CreateCachedEditor(_selectedItem.poolItem, null, ref _cachedEditor);
            VisualElement inspectorContent = _cachedEditor.CreateInspectorGUI();

            SerializedObject serializedObject = new SerializedObject(_selectedItem.poolItem);
            inspectorContent.Bind(serializedObject);
            inspectorContent.TrackSerializedObjectValue(serializedObject, so =>
            {
                _selectedItem.Name = so.FindProperty("poolingName").stringValue;
            });
            _inspectorView.Add(inspectorContent);
        }

        private void InitializeWindow()
        {
            MonoScript monoScript = MonoScript.FromScriptableObject(this);
            string scriptPath = AssetDatabase.GetAssetPath(monoScript);

            // 경로를 제대로 설정하는 과정
            _rootFolderPath = Directory.GetParent(Path.GetDirectoryName(scriptPath)).FullName.Replace("\\", "/");
            _rootFolderPath = "Assets" + _rootFolderPath.Substring(Application.dataPath.Length);

            if (poolManager == null)
            {
                string filePath = $"{_rootFolderPath}/PoolManagerSO.asset";
                poolManager = AssetDatabase.LoadAssetAtPath<PoolManagerSO>(filePath);

                if (poolManager == null) // 로드를 하려고 했는데 없으면
                {
                    Debug.LogError("PoolManagerSO not found. Creating a new one.");
                    poolManager = ScriptableObject.CreateInstance<PoolManagerSO>();
                    AssetDatabase.CreateAsset(poolManager, filePath);
                }
            }

            visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{_rootFolderPath}/Editor/PoolManagerEditor.uxml");
            Debug.Assert(visualTreeAsset != null, "Visual tree asset is null");

            itemAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{_rootFolderPath}/Editor/PoolItemUI.uxml");
            Debug.Assert(itemAsset != null, "item asset is null");

        }
    }
}