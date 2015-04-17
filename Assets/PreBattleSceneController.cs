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
    // Use this for initialization
	void Start ()
	{
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
