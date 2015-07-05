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
        myUnit.AP = new BaseAttribute(unitAttributes.AP, 2,"AP");
        myUnit.APMax = new BaseAttribute(unitAttributes.APMax, 2,"APmax");
        myUnit.HP = new BaseAttribute(unitAttributes.HP, 10,"HP");
        myUnit.HPMax = new BaseAttribute(unitAttributes.HPMax, 10,"HPmax");
        myUnit.MP = new BaseAttribute(unitAttributes.HP, 10,"MP");
        myUnit.MPMax = new BaseAttribute(unitAttributes.HPMax, 10,"MPmax");
        myUnit.Dexterity = new BaseAttribute(unitAttributes.dexterity, 10,"Dex");
        myUnit.Strenght = new BaseAttribute(unitAttributes.strenght, 10,"Str");
        myUnit.Magic = new BaseAttribute(unitAttributes.magic, 10,"Magic");
    }
}
