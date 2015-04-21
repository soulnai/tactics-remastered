using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

/*-----------------------------------------------------

    Логика боя, работа с юнитами
     
-----------------------------------------------------*/

public class BattleLogicController : Singleton<BattleLogicController>
{
    protected BattleLogicController() { } // guarantee this will be always a singleton only - can't use the constructor!

    private BattleDataController _battleData;
    // Use this for initialization
    void Start () {
        InitBattleLogic();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitBattleLogic()
    {
        _battleData = BattleDataController.Instance;
        InputController.Instance.OnTileClick += MoveUnit;
		InputController.Instance.OnUnitClick += UnitSelect;
    }

    private void MoveUnit(Tile tile)
    {
        GeneratePath(_battleData.CurrentUnit.currentTile,tile);
        MoveUnit(_battleData.CurrentUnit);
		CheckAP (_battleData.CurrentUnit);
		//_battleData.CurrentUnit.currentPath = null;
    }

	public void UnitSelect(Unit unit){
		_battleData.CurrentUnit = unit;
	}


    public void MoveUnit(Unit unit)
    {
		if (unit.AP > 0 && unit.MovementRange>=unit.currentPath.Count) {
			Vector3[] VectorPath = new Vector3[unit.currentPath.Count];
			Tile destTile = null;
			for (int i = 0; i < unit.currentPath.Count; i++) {
				VectorPath [i] = new Vector3 (unit.currentPath [i].transform.position.x, unit.currentPath [i].transform.position.y  , unit.currentPath [i].transform.position.z);
				destTile = unit.currentPath [i];
			}
			float pathTime = unit.currentPath.Count * 0.5f;
			unit.transform.DOPath (VectorPath, pathTime).OnWaypointChange(MyCallback);;
			unit.currentTile = destTile;
			foreach (Tile t in unit.currentPath){
				t.hideHighlight();
			}
			ReduceAP (_battleData.CurrentUnit);
		} else {
			Debug.Log("Недостаточно АР");
		}
    }
	//TODO fix out of bounds error
	void MyCallback(int waypointIndex) {
		_battleData.CurrentUnit.transform.LookAt (_battleData.CurrentUnit.currentPath[waypointIndex].transform.position);
	}

    public void GeneratePath(Tile from, Tile to)
    {
        Vector2[] blockedArray = null;
		foreach (Player p in _battleData.Players) {
			blockedArray = p.PartyUnits.Where (x => x.gridPosition != to.gridPosition && x.gridPosition != _battleData.CurrentUnit.gridPosition).Select (x => x.gridPosition).ToArray ();
		}
        List<Tile> path = TilePathFinder.FindPath(from, to, blockedArray, 50f);
        foreach (Tile tile in path)
        {
            tile.showHighlight(Color.red);
        }
        _battleData.CurrentUnit.currentPath = path;
    }

	public void ReduceAP(Unit unit){
		if (unit.AP > 0) {
			unit.AP = unit.AP - 1;
		}
	}

	public void CheckAP(Unit unit){
		if (unit.AP <= 0) {
			foreach (Unit u in _battleData.Players[0].SpawnedPartyUnits){
				if (u.AP > 0){
					_battleData.CurrentUnit = u;
				}
			}
		}
	}

    public void OnDestroy()
    {
        InputController.Instance.OnTileClick -= MoveUnit;
        InputController.Instance.OnUnitClick -= UnitSelect;
    }
}
