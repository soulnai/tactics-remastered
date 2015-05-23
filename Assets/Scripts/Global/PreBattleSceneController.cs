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

    private static PreBattleSceneController _Instance;
    // Use this for initialization
    public static PreBattleSceneController Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = GameObject.FindObjectOfType<PreBattleSceneController>();
            }

            return _Instance;
        }
    }

    // Use this for initialization
    void Awake()
    {
        if (_Instance == null)
        {
            //If I am the first Instance, make me the Singleton
            _Instance = this;
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _Instance)
                Destroy(this.gameObject);
        }
	}

    public void Start()
    {
        Init();
    }

    private void Init()
    {
        _globalGame = GlobalGameController.Instance;
        _battleData = BattleDataController.Instance;
        _scenes = ScenesController.Instance;
        _player = _globalGame.UserPlayer;
        AvailableUnitsPanel.PlaceUnitsInSlots(_player.AvailableUnits);
        PartyUnitsPanel.PlaceUnitsInSlots(_player.PartyUnits);
        InputController.Instance.OnUnitDropToSlot += UpdateUnitsLists;
    }

    private void UpdateUnitsLists(Unit u)
    {
        AvailableUnitsPanel.UpdateUnitsList(u);
        PartyUnitsPanel.UpdateUnitsList(u);
    }

    // Update is called once per frame
	void Update () {
	
	}

    public void OnDestroy()
    {
        InputController.Instance.OnUnitDropToSlot -= UpdateUnitsLists;
    }
}
