using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using EnumSpace;

public class Targeting : MonoBehaviour {

    public TargetType Type;
    [HideInInspector]
    public TargetOwner TargetOwner;
    public SelectionType Selection;
    //[HideInInspector]
    public int Range;
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
        Init();
	}

    public void Init()
    {
        switch (Selection)
        {
                case SelectionType.All:
                _ability.OnAbilitySelect += SelectAll;
                break;
                case SelectionType.AllInRadius:
                _ability.OnAbilitySelect += SelectSelf;
                break;
                case SelectionType.Self:
                _ability.OnAbilitySelect += SelectSelf;
                break;
                case SelectionType.Single:
                _ability.OnAbilitySelect += StartTargeting;
                    break;
        }
        if (Selection == SelectionType.Single)
        {
                    }
    }

    private void SelectAll(bool start)
    {
        if (start)
        {
            switch (Type)
            {
                case TargetType.Tile:
                    TileTargets = GM.Map.GetAllTiles();
                    break;
                case TargetType.Unit:
                    
                    break;
            }
        }
    }

    private void SelectSelf(bool start)
    {
        if (start)
        {
            switch (Type)
            {
                case TargetType.Tile:
                    SelectTile();
                    break;
                case TargetType.Unit:
                    SelectUnit();
                    break;
            }
        }
    }

    public void StartTargeting(bool start)
    {
        if (start)
        {
            Debug.Log("Targeting started");
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

    public void SelectTile(Tile tile = null)
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
            
            case SelectionType.Self:
                TileTargets.Add(GM.BattleData.CurrentUnit.CurrentTile);
                break;
        }
        _ability.OnTilesSelected(TileTargets);
    }

    public void SelectUnit(Unit unit=null)
    {
        UnitTargets.Clear();
        switch (Selection)
        {
            case SelectionType.Single:
                AddUnit(unit);           
                break;

            case SelectionType.AllInRadius:
                foreach (Unit u in GM.Map.getAllUnitsinArea(unit.CurrentTile, Radius))
                {
                    AddUnit(unit);
                }
                break;
            
            case SelectionType.Self:
                AddUnit(GM.BattleData.CurrentUnit);
                break;
                
            case SelectionType.All:
                foreach (Unit u in GM.BattleData.AllUnitsInScene)
                {
                    AddUnit(u);
                }
                break;
        }
        _ability.OnUnitsSelected(UnitTargets);
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
