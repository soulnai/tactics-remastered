using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;


/*-----------------------------------------------------

    Отвечает за описание и проверку целей и условий в даной сцене
     
-----------------------------------------------------*/

public class ScenarioController : MonoBehaviour {
	public List <List<Tile>> map = new List<List<Tile>>();
    //TODO create a list of spawn areas that will be used for different players/enemies/etc. this are for this party etc.
    public List<Tile> spawnArea = new List<Tile>();
	public int mapSize;
    //TODO Move all functions here as second class or use Spawner for all elements - Units / Props and other elements
	public UnitSpawn UnitSpawner;
	public SpawnMiscObjects ObjSpawner;
    //TODO Move to the global game data or battle data all links to the prefabs, assets, etc.


	private BattleDataController _battleData;
    private GlobalPrefabHolder _prefabHolder;

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
	void Awake(){
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

    // Инициализация сценария текущей битвы, заполнение всех полей, ссылок и пр.
    public void Init()
    {
        _battleData = BattleDataController.instance;
        _prefabHolder = GlobalPrefabHolder.instance;
        _mapController = MapUtils.Instance;
		GameObject go = ((GameObject)Instantiate(_prefabHolder.player, new Vector3(0,0,0), Quaternion.identity));
		Player player1 = go.GetComponent<Player> ();
		_battleData.Players.Add (player1);
        CreateBattleScene(_battleData.Players);
    }

    void Start () {
        Init();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateBattleScene(List<Player> players )
    {
        _mapController.loadMapFromXml();
		_mapController.loadStuffFromXml();

        for (int i = 0; i < _prefabHolder.UnitPrefabsHolder.Length; i++)
        {
			Debug.Log(spawnArea[i].gridPosition);
			Unit unit = UnitSpawner.spawnunit(spawnArea[i], _prefabHolder.UnitPrefabsHolder[i]);
			_battleData.Players[0].PartyUnits.Add(unit);
		}
		Camera.main.transform.LookAt (spawnArea[0].transform.position);
    }
}
