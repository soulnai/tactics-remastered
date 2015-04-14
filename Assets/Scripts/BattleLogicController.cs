using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

/*-----------------------------------------------------

    Логика боя, работа с юнитами
     
-----------------------------------------------------*/

public class BattleLogicController : MonoBehaviour {
    private static BattleLogicController _instance;
    private BattleDataController _battleData;
    public static BattleLogicController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<BattleLogicController>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    // Use this for initialization
    void Start () {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
        InitBattleLogic();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitBattleLogic()
    {
        _battleData = BattleDataController.instance;
        InputController.instance.OnTileClick += MoveUnit;
		InputController.instance.OnUnitClick += UnitSelect;
    }

    private void MoveUnit(Tile tile)
    {
        GeneratePath(_battleData.currentUnit.currentTile,tile);
        MoveUnit(_battleData.currentUnit);
		CheckAP (_battleData.currentUnit);
		//_battleData.currentUnit.currentPath = null;
    }

	public void UnitSelect(Unit unit){
		_battleData.currentUnit = unit;
	}


    public void MoveUnit(Unit unit)
    {
		if (unit.AP > 0 && unit.MovementRange>=unit.currentPath.Count) {
			Vector3[] VectorPath = new Vector3[unit.currentPath.Count];
			Tile destTile = null;
			for (int i = 0; i < unit.currentPath.Count; i++) {
				VectorPath [i] = new Vector3 (unit.currentPath [i].transform.position.x, unit.currentPath [i].transform.position.y + 0.5f, unit.currentPath [i].transform.position.z);
				destTile = unit.currentPath [i];
			}
			float pathTime = unit.currentPath.Count * 0.5f;
			unit.transform.DOPath (VectorPath, pathTime).OnWaypointChange(MyCallback);;
			unit.currentTile = destTile;
			foreach (Tile t in unit.currentPath){
				t.hideHighlight();
			}
			ReduceAP (_battleData.currentUnit);
		} else {
			Debug.Log("Недостаточно АР");
		}
    }
	//TODO fix out of bounds error
	void MyCallback(int waypointIndex) {
		_battleData.currentUnit.transform.LookAt (_battleData.currentUnit.currentPath[waypointIndex].transform.position);
	}

    public void GeneratePath(Tile from, Tile to)
    {
        Vector2[] blockedArray = null;
		foreach (Player p in _battleData.Players) {
			blockedArray = p.PartyUnits.Where (x => x.gridPosition != to.gridPosition && x.gridPosition != _battleData.currentUnit.gridPosition).Select (x => x.gridPosition).ToArray ();
		}
        List<Tile> path = TilePathFinder.FindPath(from, to, blockedArray, 50f);
        foreach (Tile tile in path)
        {
            tile.showHighlight(Color.red);
        }
        _battleData.currentUnit.currentPath = path;
    }

	public void ReduceAP(Unit unit){
		if (unit.AP > 0) {
			unit.AP = unit.AP - 1;
		}
	}

	public void CheckAP(Unit unit){
		if (unit.AP <= 0) {
			foreach (Unit u in _battleData.AllUnitsInScene){
				if (u.AP > 0){
					_battleData.currentUnit = u;
				}
			}
		}
	}
}
