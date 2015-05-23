using UnityEngine;
using System.Collections;
using EnumSpace;

public class AttributeChanger : MonoBehaviour {
    public unitAttributes Attribute;
    public ChangeType ChangeType;
    [HideInInspector]
    public int Rounds;
    public int Value;

    private Ability _ability;
	
	void Awake () {
        _ability = GetComponent<Ability>();
	}


}
