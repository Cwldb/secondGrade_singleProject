using KJYLib.ObjectPool.RunTime;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(PoolItemSO))]
public class PoolItemSOEditor : UnityEditor.Editor
{
    [SerializeField] private VisualTreeAsset visualTreeAsset;

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();
        visualTreeAsset.CloneTree(root);

        TextField nameField = root.Q<TextField>("PoolNameField");
        nameField.RegisterValueChangedCallback(HandleAssetNameChange);

        return root;
    }

    private void HandleAssetNameChange(ChangeEvent<string> evt)
    {
        if(string.IsNullOrEmpty(evt.newValue))
        {
            EditorUtility.DisplayDialog("Error", "Pool name cannot be empty.", "OK");
            (evt.target as TextField).SetValueWithoutNotify(evt.previousValue);
            return;
        }

        string assetPath = AssetDatabase.GetAssetPath(target);
        string newName = $"{evt.newValue}";

        string message = AssetDatabase.RenameAsset(assetPath, newName);
        if (string.IsNullOrEmpty(message))
            target.name = newName;

        else
        {
            (evt.target as TextField).SetValueWithoutNotify(evt.previousValue);
            EditorUtility.DisplayDialog("Error", message, "OK");
        }
    }
}
