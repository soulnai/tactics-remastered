using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class GlobalGameController : Singleton<GlobalGameController>
{
    protected GlobalGameController() { } // guarantee this will be always a singleton only - can't use the constructor!

    public Player UserPlayer;

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


    // Update is called once per frame
    void Update () {
	
	}
}
