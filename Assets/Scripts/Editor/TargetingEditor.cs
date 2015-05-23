using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
using EnumSpace;

[CustomEditor(typeof(Targeting))]
public class TargetingEditor : Editor {

    public override void OnInspectorGUI()
    {
          DrawDefaultInspector();

        Targeting myScript = (Targeting) target;

        switch (myScript.Selection)
        {
                case SelectionType.AllInRadius:
                myScript.Radius = EditorGUILayout.IntSlider("Radius:", myScript.Radius, 1, 10);
                break;
        }
        
        switch (myScript.Type)
        {
                case TargetType.Tile:
                
                    break;

                case TargetType.Unit:
                
                    break;
        }

        if (myScript.Selection != SelectionType.Self)
        {
            myScript.TargetOwner = (TargetOwner)EditorGUILayout.EnumPopup("TargetOwner", myScript.TargetOwner);
        }
    }
}
