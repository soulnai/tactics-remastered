using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;


/*-----------------------------------------------------

    Отвечает за описание и проверку целей и условий в даной сцене
     
-----------------------------------------------------*/

public class ScenarioController : MonoBehaviour {
	public List <List<Tile>> map;
    //TODO create a list of spawn areas that will be used for different players/enemies/etc. this are for this party etc.
    public List<Tile> spawnArea;
	public int mapSize;
    //TODO MoveTo all functions here as second class or use Spawner for all elements - Units / Props and other elements
	public UnitSpawn UnitSpawner;
	public SpawnMiscObjects ObjSpawner;

	private static ScenarioController _Instance;
	public static ScenarioController Instance
	{
		get
		{
			if (_Instance == null)
			{
				_Instance = GameObject.FindObjectOfType<ScenarioController>();
			}
			
			return _Instance;
		}
	}
	
    // Use this for initialization
	void Awake(){
		if (_Instance == null)
		{
			_Instance = this;
		}
		else
		{
			if (this != _Instance)
				Destroy(this.gameObject);
		}
	}

    // Инициализация сценария текущей битвы, заполнение всех полей, ссылок и пр.
    public void Init()
    {
        CreateBattleScene(GM.BattleData.Players);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateBattleScene(List<Player> players)
    {
        if(players.Count == 0)
            Debug.LogError("No players added to battle");

        GM.Map.loadMapFromXml("Resources/Level1/Map.xml");
        GM.Map.loadMapDetailsFromXml("Resources/Level1/mission.xml");

        GM.BattleData.Players[0].SpawnedPartyUnits.Clear();
        ;

        if (spawnArea.Count >= GM.BattleData.Players[0].PartyUnits.Count)
        {
            for (int i = 0; i < GM.BattleData.Players[0].PartyUnits.Count; i++)
            {
                Unit unit = UnitSpawner.SpawnUnit(spawnArea[i], GM.BattleData.Players[0].PartyUnits[i].gameObject, GM.BattleData.Players[0]);
                GM.BattleData.Players[0].SpawnedPartyUnits.Add(unit);
            }
        }
        else
        {
            Debug.LogError("Not enough spawn points");
        }

        placeAiUnits();
        Camera.main.transform.LookAt(spawnArea[0].transform.position);
    }

    public void placeAiUnits() 
    {
        MissionDetailsXmlContainer missionContainer = MapSaveLoad.LoadMapDetails("Resources/Level1/mission.xml");

        int unitsCount = missionContainer.players[1].units.Count;
        for (int i = 0; i < unitsCount; i++)
        {
            string stuffType = missionContainer.players[1].units[i].prefabName;
            Tile tileTospawn = GM.Scenario.map[missionContainer.players[1].units[i].locX][missionContainer.players[1].units[i].locY];
            Unit unit = UnitSpawner.SpawnUnit(tileTospawn, GM.Prefabs.Prefabs[stuffType].gameObject, GM.BattleData.Players[1]);
            GM.BattleData.Players[1].SpawnedPartyUnits.Add(unit);
        }
    }
}
