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
		//TODO replace comparasion of currentPath.Count to currentPath.Cost
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
		//Tile[] blockedArray = GetBlockedTiles ();
		List<Tile> path = TilePathFinder.FindPath(from, to, _battleData.blockedTiles.ToArray(), 0.5f);
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
			foreach (Player p in _battleData.Players){
			foreach (Unit u in p.SpawnedPartyUnits){
				if (u.AP > 0){
					_battleData.CurrentUnit = u;
						if (u.AIControlled == true){
							AITurn(u);
						}
						break;
				}
			}
			}
		}
	}

	public void AITurn(Unit unitAI){
		AIMoveToNearestEnemy (unitAI);
	}

	public void AIMoveToNearestEnemy(Unit unitAI){
		Debug.Log ("AI turn starts");
		//Tile[] blockedArray = GetBlockedTiles ();
		List<Unit> opponentsInRange = null;
		List<Tile> availableTiles = TilePathFinder.FindArea(_battleData.CurrentUnit.currentTile, _battleData.CurrentUnit.MovementRange+1, _battleData.blockedTiles.ToArray(), 100f);
		foreach (Tile t in availableTiles) {
			t.highlight.GetComponent<Renderer>().enabled = true;
		}
		foreach (Unit u in _battleData.Players[0].SpawnedPartyUnits) {
			if (u != _battleData.CurrentUnit && u.AIControlled == false){
				if (availableTiles.Contains(u.currentTile)){
					opponentsInRange.Add(u);
				}
			}
		}


		Debug.Log (availableTiles.Count);
		Debug.Log (opponentsInRange[0].name);
	}

	public Tile[] GetBlockedTiles(){
		List <Tile> tempBlocked = _battleData.blockedTiles;
		foreach (Player p in _battleData.Players) {
			foreach(Unit u in p.SpawnedPartyUnits){ 
				tempBlocked.Add(u.currentTile);
			}
		}
		return tempBlocked.ToArray();
	}

    public void OnDestroy()
    {
        InputController.Instance.OnTileClick -= MoveUnit;
        InputController.Instance.OnUnitClick -= UnitSelect;
    }
}
