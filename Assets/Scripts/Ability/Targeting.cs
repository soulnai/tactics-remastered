using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumSpace;

public class Targeting : MonoBehaviour {

    public TargetType Type;
    [HideInInspector]
    public TargetOwner TargetOwner;
    public SelectionType Selection;
    [HideInInspector]
    public int Radius;

    private List<Unit> UnitTargets;
    private List<Tile> TileTargets;

    private Ability _ability;
    // Use this for initialization
	void Awake () {
	    switch (Type)
	    {
	        case TargetType.Tile:
	            TileTargets = new List<Tile>();
	            break;
	        case TargetType.Unit:
	            UnitTargets = new List<Unit>();
	            break;
	    }
	    _ability = GetComponent<Ability>();
	}

    private void SetActive(Ability a)
    {
        if (_ability.Selected)
        {
            switch (Type)
            {
                case TargetType.Tile:
                    GM.Input.OnTileClick += SelectTile;
                    break;
                case TargetType.Unit:
                    GM.Input.OnUnitClick += SelectUnit;
                    break;
            }
        }
    }

    public void SelectTile(Tile tile)
    {
        TileTargets.Clear();
        switch (Selection)
        {
            case SelectionType.Single:
                TileTargets.Add(tile);
                break;
            case SelectionType.AllInRadius:
                TileTargets = GM.Map.getAllTilesinArea(tile, Radius);
                break;
        }
    }

    public void SelectUnit(Unit unit)
    {
        UnitTargets.Clear();
        switch (Selection)
        {
            case SelectionType.Single:
                AddUnit(unit);           
                break;

            case SelectionType.AllInRadius:
                foreach (Unit u in GM.Map.getAllUnitsinArea(unit.currentTile, Radius))
                {
                    AddUnit(unit);
                }
                break;
        }
    }

    private void AddUnit(Unit unit)
    {

        switch (TargetOwner)
        {
            case TargetOwner.Ally:
                if (unit.OwnerPlayer == GM.BattleData.CurrentUnit.OwnerPlayer)
                {
                    UnitTargets.Add(unit);
                }
                break;
            case TargetOwner.Enemy:
                if (unit.OwnerPlayer != GM.BattleData.CurrentUnit.OwnerPlayer)
                {
                    UnitTargets.Add(unit);
                }
                break;
            case TargetOwner.Any:
                UnitTargets.Add(unit);
                break;
        }
    }
}
