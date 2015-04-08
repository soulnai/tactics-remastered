using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

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
    }

    private void MoveUnit(Tile tile)
    {
        GeneratePath(_battleData.currentUnit.currentTile,tile);
        MoveUnit(_battleData.currentUnit);
		CheckAP (_battleData.currentUnit);
    }

    //TODO поворачивать юнит на каждый тайл пути
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
			unit.transform.LookAt (unit.currentPath [unit.currentPath.Count - 1].transform.position);
			unit.transform.DOPath (VectorPath, pathTime);
			unit.currentTile = destTile;
			foreach (Tile t in unit.currentPath){
				t.hideHighlight();
			}
			unit.currentPath = null;
			ReduceAP (_battleData.currentUnit);
		} else {
			Debug.Log("Недостаточно АР");
		}
    }

    public void GeneratePath(Tile from, Tile to)
    {
        //TODO replace blocked array on unitsAll.Where(x => x.gridPosition != destTile.gridPosition && x.gridPosition != currentUnit.gridPosition).Select(x => x.gridPosition).ToArray()
        Vector2[] blockedArray;
        blockedArray = new Vector2[]
        {
            new Vector2( 4, 3 ),
            new Vector2( 8, 2 ),
        };
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
		Debug.Log (_battleData.AllUnitsInScene.Count);
		if (unit.AP <= 0) {
			foreach (Unit u in _battleData.AllUnitsInScene){
				if (u.AP > 0){
					_battleData.currentUnit = u;
				}
			}
		}
	}
}
