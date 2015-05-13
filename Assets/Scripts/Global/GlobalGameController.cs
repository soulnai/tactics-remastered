using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class GlobalGameController : Singleton<GlobalGameController>
{
    protected GlobalGameController() { } // guarantee this will be always a singleton only - can't use the 

    public Player UserPlayer;
	public Player AIPlayer;

    private bool _wasInited = false;

    // Use this for initialization
    void Awake()
    {

    }

    public void Start()
    {
        if (!_wasInited)
            Init();
    }

    private void Init()
    {
        //TODO if need load then load player from XML etc. else create new User player
        CreateUserPlayer();
		CreateAIPlayer();
        _wasInited = true;
    }

    private void CreateUserPlayer()
    {
        GameObject tempPlayer = (GameObject)Instantiate(GlobalPrefabHolder.Instance.Prefabs["Player"], Vector3.zero, Quaternion.identity);
        UserPlayer = tempPlayer.GetComponent<Player>();
        UserPlayer.transform.SetParent(this.transform);
        //TODO replace with final solution
        UserPlayer.name = "UserPlayer";
        UserPlayer.PlayerName = "Bob";
        UserPlayer.AvailableUnits.Add(GlobalPrefabHolder.Instance.Prefabs["Warrior_01"].GetComponent<Unit>());
        UserPlayer.AvailableUnits.Add(GlobalPrefabHolder.Instance.Prefabs["Warrior_02"].GetComponent<Unit>());
        UserPlayer.AvailableUnits.Add(GlobalPrefabHolder.Instance.Prefabs["Warrior_01"].GetComponent<Unit>());
        UserPlayer.AvailableUnits.Add(GlobalPrefabHolder.Instance.Prefabs["Warrior_02"].GetComponent<Unit>());
    }

	private void CreateAIPlayer()
	{
		GameObject tempPlayer = (GameObject)Instantiate(GlobalPrefabHolder.Instance.Prefabs["Player"], Vector3.zero, Quaternion.identity);
		AIPlayer = tempPlayer.GetComponent<Player>();
		AIPlayer.transform.SetParent(this.transform);
		//TODO replace with final solution
		AIPlayer.name = "AIPlayer";
		AIPlayer.PlayerName = "Artifica";
		loadAIunitsDetailsFromXml("Resources/Level1/mission.xml");
		foreach (Unit u in AIPlayer.AvailableUnits) {
			AIPlayer.PartyUnits.Add (u);
		}
	}

	public void loadAIunitsDetailsFromXml(string mapDetailesfile)
	{
		MissionDetailsXmlContainer container = MapSaveLoad.LoadMapDetails (mapDetailesfile);
		
		for (int j = 0; j < container.players[1].units.Count; j++) {
			AIPlayer.AvailableUnits.Add (GlobalPrefabHolder.Instance.Prefabs [container.players [1].units [j].prefabName].GetComponent<Unit> ());
			Debug.Log (container.players [1].units [j].prefabName);
		}
	}
    // Update is called once per frame
    void Update () {
	
	}
}
