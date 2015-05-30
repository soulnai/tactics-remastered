using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
using EnumSpace;

[CustomEditor(typeof(AttributeChanger))]
public class AttributeChangerEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AttributeChanger myScript = (AttributeChanger)target;

        if (myScript.ChangeType == ChangeType.EachTurn)
        {
            myScript.Turns = EditorGUILayout.IntSlider("Turns:", myScript.Turns, 1, 10);
        }
    }
}
