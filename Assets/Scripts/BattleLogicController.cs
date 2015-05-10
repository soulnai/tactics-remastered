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

	public int CalcPathCost(Unit unit){
		int PathCost = 0;
		for (int i = 0; i < unit.currentPath.Count; i++) {
			PathCost+= unit.currentPath[i].movementCost;
		}
		return PathCost;
	}

    public void MoveUnit(Unit unit)
    {
		if (unit.AP > 0 && unit.MovementRange>=CalcPathCost(unit)) {
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
			NextUnit();
		}
		if (unit.AP > 0 && unit.AIControlled == true) {
			AITurn(unit);
		}
	}

	public void NextUnit () {
		foreach (Unit u in _battleData.currentPlayer.SpawnedPartyUnits) {
			if (u.AP > 0) {
				_battleData.CurrentUnit = u;
				if (_battleData.CurrentUnit.AIControlled == true){
					AITurn(_battleData.CurrentUnit);
				}
				break;
			}
			NextPlayer();
		}
	}

	public void NextPlayer () {
		foreach (Player p in _battleData.Players) {
			if (p != _battleData.currentPlayer){
				_battleData.currentPlayer = p;
				break;
			}
		}
	}
	public void AITurn(Unit unitAI){
		AIMoveToNearestEnemy (unitAI);
		CheckAP (unitAI);
	}

	public void AIMoveToNearestEnemy(Unit unitAI){
		Debug.Log ("AI turn starts");
		List<Unit> opponentsInRange = new List<Unit>();

		foreach (Unit u in _battleData.Players[0].SpawnedPartyUnits) {
			if (u != _battleData.CurrentUnit && u.AIControlled == false) {
				opponentsInRange.Add (u);
			}
		}
		//find nearest opponent
		Unit opponent = opponentsInRange.OrderBy (x => x != null ? -x.HP : 1000).ThenBy (x => x != null ? TilePathFinder.FindPath(unitAI.currentTile, x.currentTile, _battleData.blockedTiles.ToArray(), 100f).Count() : 1000).First ();
		List<Tile> pathToOpponent = TilePathFinder.FindPath(unitAI.currentTile, opponent.currentTile, _battleData.blockedTiles.ToArray(), 100f);

		unitAI.currentPath = pathToOpponent;
		if (CalcPathCost (unitAI) > unitAI.MovementRange) {
			Debug.Log("Path reduce");
			int movementCost = 0;
			int pathEndPoint;
			for(int i=0;i<pathToOpponent.Count;i++)
			{
				movementCost +=pathToOpponent[i].movementCost;
				if(movementCost > unitAI.MovementRange)
				{
					int countToRemove = pathToOpponent.Count - i;
					pathToOpponent.RemoveRange(i, countToRemove);
				}
			}
		} else {
			pathToOpponent.Remove (pathToOpponent [pathToOpponent.Count - 1]);
		}

		MoveUnit (unitAI);
	}

	public Tile[] GetBlockedTiles(Unit exludeUnit){
		List <Tile> tempBlocked = new List<Tile>(_battleData.blockedTiles);
		foreach (Player p in _battleData.Players) {
			foreach(Unit u in p.SpawnedPartyUnits){ 
				if (u == exludeUnit) continue;
				tempBlocked.Add(u.currentTile);
			}
		}
		return tempBlocked.ToArray();
	}

	public List<Unit> getAllUnitsinArea(Tile center, int radius){
		List<Tile> availableTiles = new List<Tile>();
		List<Unit> opponentsInRange = new List<Unit>();
		//find all available tiles in area
		availableTiles = TilePathFinder.FindArea(center, radius, _battleData.blockedTiles.ToArray(), 100f);
		foreach (Tile t in availableTiles) {
			t.highlight.GetComponent<Renderer>().enabled = true;
		}
		//find all units in area
		foreach (Unit u in _battleData.Players[0].SpawnedPartyUnits) {
			if (u != _battleData.CurrentUnit && u.AIControlled == false){
				if (availableTiles.Contains(u.currentTile)){
					opponentsInRange.Add(u);
				}
			}
		}
		return opponentsInRange;
	}

    public void OnDestroy()
    {
        InputController.Instance.OnTileClick -= MoveUnit;
        InputController.Instance.OnUnitClick -= UnitSelect;
    }
}
