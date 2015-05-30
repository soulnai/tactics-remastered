using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public Action<bool> OnAbilitySelect;
    public Action<List<Unit>> OnUnitsSelect;
    public Action<List<Tile>> OnTilesSelect;

    public string AbilityName;
    public Sprite Icon;

    [HideInInspector]
    public bool Selected;

    public void OnUnitsSelected(List<Unit> units)
    {
        if (OnUnitsSelect != null)
        {
            OnUnitsSelect(units);
        }
    }

    public void OnTilesSelected(List<Tile> tiles)
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
        }
        else
        {
            Selected = false;
        }
        if (OnAbilitySelect != null)
        {
            OnAbilitySelect(Selected);
        }
    }
}
