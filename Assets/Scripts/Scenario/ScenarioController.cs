﻿using UnityEngine;
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
    //TODO MoveTo all functions here as second class or use Spawner for all elements - Units / Props and other elements
	public UnitSpawn UnitSpawner;
	public SpawnMiscObjects ObjSpawner;

	private BattleDataController _battleData;
    private GlobalPrefabHolder _prefabHolder;

	private static ScenarioController _Instance;
	public static ScenarioController Instance
	{
		get
		{
			if (_Instance == null)
			{
				_Instance = GameObject.FindObjectOfType<ScenarioController>();
				
				//Tell unity not to destroy this object when loading a new scene!
				//DontDestroyOnLoad(_Instance.gameObject);
			}
			
			return _Instance;
		}
	}

    private MapUtils _mapController;
	
    // Use this for initialization
	void Awake(){
		if (_Instance == null)
		{
			//If I am the first Instance, make me the Singleton
			_Instance = this;
			//DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if (this != _Instance)
				Destroy(this.gameObject);
		}
	}

    // Инициализация сценария текущей битвы, заполнение всех полей, ссылок и пр.
    public void Init()
    {
        _battleData = BattleDataController.Instance;
        _prefabHolder = GlobalPrefabHolder.Instance;
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
		_mapController.loadMapFromXml("Resources/Level1/Map.xml");
		_mapController.loadMapDetailsFromXml("Resources/Level1/mission.xml");
		int t = 0;
		foreach (Unit u in _battleData.Players[1].PartyUnits){
			Unit unitAI = UnitSpawner.SpawnUnit(map[5][11+t], u.gameObject);
			unitAI.AIControlled = true;
			_battleData.Players[1].SpawnedPartyUnits.Add(unitAI);
			t++;
		}


        if (_battleData.Players.Count > 0)
        {
            if (spawnArea.Count >= _battleData.AllUnitsInScene.Count)
            {
                for (int i = 0; i < _battleData.Players[0].PartyUnits.Count; i++)
                {
                    Unit unit = UnitSpawner.SpawnUnit(spawnArea[i], _battleData.Players[0].PartyUnits[i].gameObject);
					_battleData.Players[0].SpawnedPartyUnits.Add(unit);
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
		_battleData.CurrentUnit = _battleData.Players [0].SpawnedPartyUnits [0];

        Camera.main.transform.LookAt (spawnArea[0].transform.position);
    }
}
