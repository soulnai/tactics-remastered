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

	private BattleDataController _battleData;

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
	private MapUtils _mapController;
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
        _mapController = MapUtils.Instance;
		Init();
		CreateBattleScene (_battleData.Players);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Init()
    {
		_battleData = BattleDataController.instance;
    }

    public void CreateBattleScene(List<Player> players )
    {
        _mapController.loadMapFromXml();
		_mapController.loadStuffFromXml();
		   
        _battleData.Players[0].PartyUnits.Add(UnitSpawner.spawnunit(map[11][11], 0));
        _battleData.Players[0].PartyUnits.Add(UnitSpawner.spawnunit(map[9][9], 1));
    }
}
