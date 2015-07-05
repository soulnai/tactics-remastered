using UnityEngine;
using System.Collections.Generic;
using EnumSpace;

/*-----------------------------------------------------

    Отвечает за хранение всей информации по текущей битве:
        *количество сторон
        *количество и тип юнитов у каждой из сторон
        *контроль ходов

-----------------------------------------------------*/

public class BattleDataController : MonoBehaviour
{
    public List<Player> Players;
	public List<Tile> allTiles;
    public int currentRound = 0;
    public unitTurnStates UnitControlState = unitTurnStates.none;

    private Unit _currentUnit;
    public Unit CurrentUnit
    {
        get { return _currentUnit; }
        set { _currentUnit = value;
            GM.Events.CurrentUnitChanged(_currentUnit);
        }
    }

    public Player currentPlayer;
	public List<Tile> blockedTiles;
    public bool ReadyToStart = false;

    public List<Unit> AllUnitsInScene
    {
        get
        {
            List<Unit> _allUnits = new List<Unit>();
            foreach (Player p in Players)
            {
                foreach (Unit u in p.PartyUnits)
                {
                    _allUnits.Add(u);
                }
            }
            return _allUnits;
        }
    }

    private static BattleDataController _Instance;



    public static BattleDataController Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = GameObject.FindObjectOfType<BattleDataController>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_Instance.gameObject);
            }

            return _Instance;
        }
    }


    void Awake()
    {
        if (_Instance == null)
        {
            //If I am the first Instance, make me the Singleton
            _Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _Instance)
                Destroy(this.gameObject);
        }
        DontDestroyOnLoad(_Instance.gameObject);
    }
    // Use this for initialization
    void Start()
    {
        ScenesController.Instance.OnBattleSceneLoadingStart += CheckBattleData;
    }

    private void CheckBattleData()
    {
        Players = new List<Player>();
        if (GM.GlobalGame.UserPlayer != null)
        {
            Players.Add(GM.GlobalGame.UserPlayer);
            Players.Add(GM.GlobalGame.AIPlayer);
        }
        if ((Players != null)&&(Players.Count>0))
        {
            if (Players[0].PartyUnits.Count > 0)
            {
                ReadyToStart = true;
                currentPlayer = Players[0];
            }
            else
            {
                Debug.LogError("No units or players added to battle data");
            }
        }
    }

    public void Init()
    {
        currentRound = 0;
        currentPlayer = Players[0];
        CurrentUnit = Players[0].SpawnedPartyUnits[0];
    }

    public Unit NextUnit
    {
        get
        {
            foreach (Unit u in currentPlayer.SpawnedPartyUnits)
            {
                if (u.AP.Value > 0)
                {
                    return u;
                }
            }
            return null;
        }
    }

    public Player NextPlayer
    {
        get
        {
            if (Players.FindIndex(x => x == currentPlayer) < Players.Count - 1)
            {
                return Players[Players.FindIndex(x => x == currentPlayer) + 1];
            }
            else
            {
                return Players[0];
            }
        }
    }

    public Unit NextUnitWithAP
    {
        get
        {
            foreach (Unit unit in currentPlayer.SpawnedPartyUnits)
            {
                if (unit.AP.Value > 0)
                {
                    return unit;
                }
            }
            return null;
        }
    }
}
