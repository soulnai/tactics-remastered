using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class GlobalGameController : MonoBehaviour
{
    public Player UserPlayer;

    private bool _wasInited = false;
    private static GlobalGameController _instance;
    
    public static GlobalGameController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GlobalGameController>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    // Use this for initialization
    void Awake()
    {
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
    }

    private void CreateUserPlayer()
    {
        GameObject tempPlayer = (GameObject)Instantiate(GlobalPrefabHolder.instance.Prefabs["Player"], Vector3.zero, Quaternion.identity);
        UserPlayer = tempPlayer.GetComponent<Player>();
        UserPlayer.transform.SetParent(this.transform);
        //TODO replace with final solution
        UserPlayer.name = "UserPlayer";
        UserPlayer.PlayerName = "Bob";
        UserPlayer.AvailableUnits.Add(GlobalPrefabHolder.instance.Prefabs["Warrior_01"].GetComponent<Unit>());
        UserPlayer.AvailableUnits.Add(GlobalPrefabHolder.instance.Prefabs["Warrior_02"].GetComponent<Unit>());
        UserPlayer.AvailableUnits.Add(GlobalPrefabHolder.instance.Prefabs["Warrior_01"].GetComponent<Unit>());
        UserPlayer.AvailableUnits.Add(GlobalPrefabHolder.instance.Prefabs["Warrior_02"].GetComponent<Unit>());
    }


    // Update is called once per frame
    void Update () {
	
	}
}
