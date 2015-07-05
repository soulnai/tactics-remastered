using UnityEngine;
using System.Collections;
using UnityEditor;
using EnumSpace;

[CustomEditor((typeof(Unit)))]
public class UnitEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();

        Unit myUnit = (Unit)target;
        if (GUILayout.Button("Init Attributes"))
        {
            CreateBaseAttributes(myUnit);
        }

    }

    private void CreateBaseAttributes(Unit myUnit)
    {
        myUnit.AP.Init(unitAttributes.AP, 2,"AP");
        myUnit.APMax.Init(unitAttributes.APMax, 2, "APmax");
        myUnit.HP.Init(unitAttributes.HP, 10, "HP");
        myUnit.HPMax.Init(unitAttributes.HPMax, 10, "HPmax");
        myUnit.MP.Init(unitAttributes.HP, 10, "MP");
        myUnit.MPMax.Init(unitAttributes.HPMax, 10, "MPmax");
        myUnit.Dexterity.Init(unitAttributes.dexterity, 10, "Dex");
        myUnit.Strenght.Init(unitAttributes.strenght, 10, "Str");
        myUnit.Magic.Init(unitAttributes.magic, 10, "Magic");
    }
}
