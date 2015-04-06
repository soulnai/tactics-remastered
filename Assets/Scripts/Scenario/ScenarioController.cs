using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*-----------------------------------------------------

    Отвечает за описание и проверку целей и условий в даной сцене
     
-----------------------------------------------------*/

public class ScenarioController : MonoBehaviour {
	public List <List<Tile>> map = new List<List<Tile>>();
	public int mapSize;

	public UnitSpawn UnitSpawner;
	public GameObject[] UnitPrefabsHolder;
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
		UnitSpawner.spawnunit (map[9][9]);

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
		if (curentUnit.currentPath.Count > 0) {
			Debug.Log(curentUnit.currentPath.Count);
			foreach (Tile t in curentUnit.currentPath){
				Debug.Log(curentUnit.currentPath[0].transform.position);
				transform.position += (curentUnit.currentPath[0].transform.position - curentUnit.transform.position).normalized * curentUnit.speed * Time.deltaTime;
				if (Vector3.Distance(curentUnit.currentPath[0].transform.position, curentUnit.transform.position) <= 0.1f) {
					curentUnit.currentPath.RemoveAt(0);
				}
			}
		}
	}


}
