using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumSpace;
using System;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class BaseAttribute : ICloneable {
	public string name;
	public unitAttributes attribute;
	public int _value;
	public int Value{
		get{return _value;}
		set{
			_value = value;
			//if(owner != null)
				//TODO EventManager.UnitAttributeChanged(owner,this);
		}
	}

	private Unit owner;

   /* internal static float GetMaxAttributeValue(BaseAttribute at)
    {
        if (at.owner != null)
        {
            switch (at.attribute)
            {
                case unitAttributes.AP:
                    return at.owner.getAttribute(unitAttributes.APmax).valueMod;
                case unitAttributes.HP:
                    return at.owner.getAttribute(unitAttributes.HPmax).valueMod;
                case unitAttributes.MP:
                    return at.owner.getAttribute(unitAttributes.MPmax).valueMod;
            }
        }
        return at.valueMod;
    }
     */

	public object Clone()
	{
		return this.MemberwiseClone();
	}
}
