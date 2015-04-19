using UnityEngine;
using System.Collections;

public class PreBattleSceneController : MonoBehaviour
{
    public UnitsPanel AvailableUnitsPanel;
    public UnitsPanel PartyUnitsPanel;
    public GameObject UnitListItem;

    private Player _player;
    private GlobalGameController _globalGame;
    private BattleDataController _battleData;
    private ScenesController _scenes;

    private static PreBattleSceneController _instance;
    // Use this for initialization
    public static PreBattleSceneController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PreBattleSceneController>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    // Use this for initialization
    void Start()
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
        _globalGame = GlobalGameController.instance;
        _battleData = BattleDataController.instance;
        _scenes = ScenesController.instance;
        _player = _globalGame.UserPlayer;
        AvailableUnitsPanel.PlaceUnitsInSlots(UnitListItem);
        InputController.instance.OnUnitDropToSlot += UpdateUnitsLists;
    }

    private void UpdateUnitsLists(Unit u)
    {
        AvailableUnitsPanel.UpdateUnitsList(u);
        PartyUnitsPanel.UpdateUnitsList(u);
    }

    // Update is called once per frame
	void Update () {
	
	}
}
