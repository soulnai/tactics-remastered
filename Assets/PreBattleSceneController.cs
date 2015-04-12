using UnityEngine;
using System.Collections;

public class PreBattleSceneController : MonoBehaviour
{
    public UnitsPanel AvailableUnitsPanel;
    public UnitsPanel PartyUnitsPanel;
    public UnitListElement UnitListElement;

    private Player _player;
    private GlobalGameController _globalGame;
	// Use this for initialization
	void Start ()
	{
	    Init();
	}

    private void Init()
    {
        _globalGame = GlobalGameController.instance;
        _player = _globalGame.Player;
        AvailableUnitsPanel.PlaceUnitsInSlots(UnitListElement);
    }

    // Update is called once per frame
	void Update () {
	
	}
}
