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
		_mapController.loadMapFromXml("Resources/Level1/map.xml");
		_mapController.loadMapDetailsFromXml("Resources/Level1/mission.xml");

        if (_battleData.Players.Count > 0)
        {
            if (spawnArea.Count >= _battleData.AllUnitsInScene.Count)
            {
                for (int i = 0; i < _battleData.Players[0].PartyUnits.Count; i++)
                {
                    Unit unit = UnitSpawner.SpawnUnit(spawnArea[i], _battleData.Players[0].PartyUnits[i].gameObject);
                }
            }
            else
            {
                Debug.LogError("Not enough spawn points");
            }
        }
        else
        {
            Debug.LogError("No players added to battle data controller");
        }

        Camera.main.transform.LookAt (spawnArea[0].transform.position);
    }
}
