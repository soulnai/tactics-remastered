using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumSpace;
using System;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class BaseAttribute {
	public string name;
	public unitAttributes attribute;
//    public Unit owner;

	public int _value;
	public int Value{
		get{return _value;}
		set{
            if(GM.Events != null)
                GM.Events.UnitAttributeChanged(this, (float)_value, (float)value);
   			_value = value;
		}
	}

    public BaseAttribute(unitAttributes attr, int val,string n = "")
    {
        name = n;
        attribute = attr;
        Value = val;
    }
}
