using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class GlobalGameController : MonoBehaviour
{
    public Player Player;
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

        Init();
    }

    private void Init()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Player = new Player();
        Player.PlayerName = "Bob";
        Player.AvailableUnits.Add(GlobalPrefabHolder.instance.UnitKnightPrefab);
        Player.AvailableUnits.Add(GlobalPrefabHolder.instance.UnitScoutPrefab);
    }


    // Update is called once per frame
    void Update () {
	
	}
}
