using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
        _ability.OnUnitsSelect += ApplyToSelectedUnits;
	}

    private void ApplyToSelectedUnits(List<Unit> units)
    {
        if (GetComponent<Cost>().CanPayCost())
        {
            foreach (Unit unit in units)
            {
                BaseAttribute attr = unit.GetAttribute(Attribute);
                ApplyToAttribute(attr);
            }
        }
        _ability.Applied();
    }

    private void ApplyToAttribute(BaseAttribute attr)
    {
        switch (ModType)
        {
            case ModType.Add:
                attr.Value += Value;
                break;
            case ModType.Sub:
                attr.Value -= Value;
                break;
            case ModType.Percent:
                attr.Value = attr.Value*Value;
                break;
            case ModType.Change:
                attr.Value = Value;
                break;
        }
    }
}
