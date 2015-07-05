using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using EnumSpace;

[System.Serializable]
public class UnitOld : MonoBehaviour {

	public Sprite icon;
    public Sprite iconDead;

	public bool isFlying = false;

	public Vector2 gridPosition = Vector2.zero;
	
	public Vector3 moveDestination;
	public float moveSpeed = 5.0f;
	
	//public int movementPerActionPoint = 5;
	public int attackRange = 1;
	public int attackDistance = 5;

	public string unitName = "George";
	public List<BaseAttribute> attributes = new List<BaseAttribute>();

	//Attributes
	public int movementPerActionPoint{
		set{
            setAttribute(unitAttributes.movementDistance, value);
		}
		get{
            return getAttribute(unitAttributes.movementDistance).Value;
		}
	}
	public int HPmax{
		get{
            return getAttribute(unitAttributes.HPMax).Value;
		}
	}
	public int HP{
		set{
			setAttribute(unitAttributes.HP,value);
		}
		get{
            return getAttribute(unitAttributes.HP).Value;
		}
	}
	public int APmax{
		get{
            return getAttribute(unitAttributes.APMax).Value;
		}
	}
	public int AP{
		set{
			setAttribute(unitAttributes.AP,value);
		}
		get{
            return getAttribute(unitAttributes.AP).Value;
		}
	}
	public int MPmax{
		get{
            return getAttribute(unitAttributes.MPMax).Value;
		}
	}
	public int MP{
		set{
			setAttribute(unitAttributes.MP,value);
		}
		get{
            return getAttribute(unitAttributes.MP).Value;
		}
	}
	public int Strength{
		get{
            return getAttribute(unitAttributes.strenght).Value;
		}
	}
	public int Dexterity{
		get{
            return getAttribute(unitAttributes.dexterity).Value;
		}
	}
	public int Magic{
		get{
            return getAttribute(unitAttributes.magic).Value;
		}
	}
	public int PhysicalDef{
		set{
			setAttribute(unitAttributes.PhysicalDef,value);
		}
		get{
            return getAttribute(unitAttributes.PhysicalDef).Value;
		}
	}
	public int MagicDef{
		set{
			setAttribute(unitAttributes.magicDef,value);
		}
		get{
            return getAttribute(unitAttributes.magicDef).Value;
		}
	}

	public float maxHeightDiff = 0.5f;
	public EnumSpace.unitClass UnitClass;
	public EnumSpace.unitStates UnitState = unitStates.normal;
	public EnumSpace.unitActions UnitAction = unitActions.idle;
	public Tile currentTile;

	public Unit currentTarget;

	public Player playerOwner;


	public Dictionary<unitAttributes,BaseAttribute> attributesDictionary = new Dictionary<unitAttributes, BaseAttribute>();
	public Dictionary<unitAttributes,BaseAttribute> attributesModDictionary = new Dictionary<unitAttributes, BaseAttribute>();

	void Awake () {

	}

	void clampAttributeLimits (Unit owner, BaseAttribute at)
	{
		if(owner == this){
			if(at.attribute == unitAttributes.HPMax)
				HP = Mathf.Clamp(HP,0,HPmax);
			if(at.attribute == unitAttributes.MPMax)
				MP = Mathf.Clamp(MP,0,MPmax);
			if(at.attribute == unitAttributes.APMax)
				AP = Mathf.Clamp(AP,0,APmax);
		}
	}

	public BaseAttribute getAttribute(unitAttributes a)
	{
		return attributes.Find(BaseAttribute => BaseAttribute.attribute == a);
	}

	public void setAttribute(unitAttributes a,int val)
	{
		foreach(BaseAttribute at in attributes)
		attributes.Find(BaseAttribute => BaseAttribute.attribute == a).Value = val;
	}
}
