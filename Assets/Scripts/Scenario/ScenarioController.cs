using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;


/*-----------------------------------------------------

    Отвечает за описание и проверку целей и условий в даной сцене
     
-----------------------------------------------------*/

public class ScenarioController : MonoBehaviour {
	public List <List<Tile>> map = new List<List<Tile>>();
	public int mapSize;

	public UnitSpawn UnitSpawner;
	public SpawnMiscObjects ObjSpawner;
	public GameObject[] UnitPrefabsHolder;
	public GameObject[] MiscPrefabsHolder;
	public List<Unit> unitsAll;
	public Unit curentUnit;

	private static ScenarioController _instance;
	public static ScenarioController instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<ScenarioController>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}
	public MapUtils MapController;
	// Use this for initialization
	void OnAwake(){
		if (_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if (this != _instance)
				Destroy(this.gameObject);
		}
	}

	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Init()
    {

        MapController.loadMapFromXml();
		UnitSpawner.spawnunit (map[9][9], 1);
		ObjSpawner.SpawnMapObject (map[8][8], 0);
		ObjSpawner.SpawnMapObject (map[2][2], 1);
		ObjSpawner.SpawnMapObject (map[12][12], 2);
		ObjSpawner.SpawnMapObject (map[8][4], 3);
		ObjSpawner.SpawnMapObject (map[1][12], 5);
		ObjSpawner.SpawnMapObject (map[2][12], 6);
		ObjSpawner.SpawnMapObject (map[6][6], 7);


    }

	public void GeneratePath(Tile from, Tile to){
		//TODO replace blocked array on unitsAll.Where(x => x.gridPosition != destTile.gridPosition && x.gridPosition != currentUnit.gridPosition).Select(x => x.gridPosition).ToArray()
		Vector2[] blockedArray;
		blockedArray = new Vector2[]
		{
			new Vector2( 4, 3 ),
			new Vector2( 8, 2 ),
		};
		List<Tile> path = TilePathFinder.FindPath (from, to, blockedArray, 50f);
		foreach( Tile tile in path )
		{
			tile.showHighlight(Color.red);
		}
		curentUnit.currentPath = path;
		MoveUnit (curentUnit);
	}

	//TODO все работает, но юнит не ходит :)
	public void MoveUnit (Unit unit){
		Vector3[] VectorPath = new Vector3 [unit.currentPath.Count];
		Tile destTile = null;
		for (int i = 0; i<unit.currentPath.Count; i++){
			VectorPath[i] = new Vector3(unit.currentPath[i].transform.position.x,unit.currentPath[i].transform.position.y+0.5f,unit.currentPath[i].transform.position.z);
			destTile = unit.currentPath[i];
		}
		unit.transform.LookAt (unit.currentPath[unit.currentPath.Count-1].transform.position);
		unit.transform.DOPath (VectorPath, 3f);
		unit.currentTile = destTile;
		unit.currentPath = null;
	}


}
