using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumSpace;
using UnityEngine.Events;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public Action<bool> OnAbilitySelect;
    public Action<List<Unit>> OnUnitsSelect;
    public Action<List<Tile>> OnTilesSelect;
    public Action OnApply;


    public string AbilityName;
    public Sprite Icon;

    private bool _selected;

    [HideInInspector]
    public bool Selected
    {
        get
        {
            return _selected; 
            
        }
        set
        {
            if (_selected != value)
            {
                AbilitySelected(value);
                _selected = value;
            }
        }
    }
    
    [HideInInspector]
    public Unit Owner;

    public void Init(Unit u)
    {
        Owner = u;
    }

    public void AbilitySelected(bool s)
    {
        if (OnAbilitySelect != null)
            OnAbilitySelect(s);
    }

    public void UnitsSelected(List<Unit> units)
    {
        if (OnUnitsSelect != null)
        {
            OnUnitsSelect(units);
        }
    }

    public void TilesSelected(List<Tile> tiles)
    {
        if (OnTilesSelect != null)
        {
            OnTilesSelect(tiles);
        }
    }

	// Use this for initialization
	void Start () {
        GM.Input.OnAbilityClick += SetSelected;
	}
	
	// Update is called once per frame
    public void SetSelected(Ability a)
    {
        if (this == a)
        {
            Selected = true;
            Owner.CurrentAction = unitActions.readyToAttack;
        }
        else
        {
            Selected = false;
        }
    }

    public void Applied()
    {
        if (OnApply != null)
        {
            OnApply();
        }
        Owner.CurrentAction = unitActions.idle;
        Selected = false;
    }
}
