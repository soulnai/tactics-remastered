using UnityEngine;
using System.Collections;
using EnumSpace;
//using UnityEditor;

public class AttributeChanger : MonoBehaviour {
    public unitAttributes Attribute;
    public ChangeType ChangeType;
    public ModType ModType;
    public BuffType BuffType;
    [HideInInspector]
    public int Turns;
    public int Value;

    private Ability _ability;
	
	void Awake () {
        _ability = GetComponent<Ability>();
	}


}
