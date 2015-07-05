using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Ability))]
public class AbilityEditor : Editor {

    public override void OnInspectorGUI()
    {
        Ability myScript = (Ability)target;
        
        Texture2D myTexture = AssetPreview.GetAssetPreview(myScript.Icon);

        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(myTexture, GUILayout.Height(64),GUILayout.Width(64));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(myScript.AbilityName);
        GUILayout.FlexibleSpace();  
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

        DrawDefaultInspector();
    }
}
