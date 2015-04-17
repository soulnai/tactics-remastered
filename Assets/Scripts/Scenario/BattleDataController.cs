﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*-----------------------------------------------------

    Отвечает за хранение всей информации по текущей битве:
        *количество сторон
        *количество и тип юнитов у каждой из сторон
        *контроль ходов

-----------------------------------------------------*/

public class BattleDataController : MonoBehaviour
{
    public List<Player> Players;
    public int currentRound = 0;
    public Unit CurrentUnit;
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

    private static BattleDataController _instance;


    public static BattleDataController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<BattleDataController>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }


    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        Players.Add(GlobalGameController.instance.UserPlayer);
        ScenesController.instance.OnBattleSceneLoadingStart += CheckBattleData;
    }

    private void CheckBattleData()
    {
        if ((Players != null)&&(Players.Count>0))
        {
            if (Players[0].PartyUnits.Count > 0)
            {
                ReadyToStart = true;
            }
            else
            {
                Debug.LogError("No units or players added to battle data");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
