using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

/*-----------------------------------------------------

    Логика боя, работа с юнитами
     
-----------------------------------------------------*/

public class BattleLogicController : Singleton<BattleLogicController>
{
    //player turn events
    public Action<Player> OnPlayerTurnEnd;
    public Action<Player> OnPlayerTurnStart;
    //unit turn events
    public Action<Unit> OnUnitTurnEnd;
    public Action<Unit> OnUnitTurnStart;
    //change action state
    public Action<Unit, EnumSpace.unitActions> OnUnitStateChange;
    // receive ability
    public Action<List<Unit>,AbilityContainer> OnUnitAbilityReceived;


    protected BattleLogicController() { } // guarantee this will be always a singleton only - can't use the constructor!

    // Use this for initialization
    void Start () {
        InitBattleLogic();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitBattleLogic()
    {
        InputController.Instance.OnTileClick += TileClick;
		InputController.Instance.OnUnitClick += SetCurrentUnit;
    }

    public void TileClick(Tile tile)
    {
        GM.BattleData.CurrentUnit.Move(tile);
    }

    public void SetCurrentUnit(Unit unit){
		GM.BattleData.CurrentUnit = unit;
	}




	
    //TODO fix out of bounds error
	



    public void CheckAP(Unit unit)
    {
        if (unit.AP <= 0)
        {
            NextUnit();
        }
        if (unit.AP > 0 && unit.AIControlled == true)
        {
            AITurn(unit);
        }
    }

	public void NextUnit () {
		foreach (Unit u in GM.BattleData.currentPlayer.SpawnedPartyUnits) {
			if (u.AP > 0) {
				GM.BattleData.CurrentUnit = u;
				if (GM.BattleData.CurrentUnit.AIControlled == true){
					AITurn(GM.BattleData.CurrentUnit);
				}
				break;
			}
				NextPlayer();
		}
	}

	public void NextPlayer () {
		bool changePlayer = false;
		foreach (Unit u in GM.BattleData.currentPlayer.SpawnedPartyUnits) {
			if (u.AP > 0) {
				changePlayer = false;
				break;
			} else {
				changePlayer = true;
			}
		}

		if (changePlayer == true) {
			foreach (Player p in GM.BattleData.Players) {
                if (p != GM.BattleData.currentPlayer)
                {
                    GM.BattleData.currentPlayer = p;
					break;
				}
			}
            GM.BattleData.CurrentUnit = GM.BattleData.currentPlayer.SpawnedPartyUnits[0];
            AITurn(GM.BattleData.currentPlayer.SpawnedPartyUnits[0]);
		}
	}

	public void AITurn(Unit unitAI){
		AIMoveToNearestEnemy (unitAI);
		CheckAP (unitAI);
	}

	public List<Unit> FindAllPlayerUnits(){
		List<Unit> opponentsInRange = new List<Unit>();

        foreach (Player p in GM.BattleData.Players)
        {
            if (p != GM.BattleData.currentPlayer)
            {
				foreach (Unit u in p.SpawnedPartyUnits) {
                    if (u != GM.BattleData.CurrentUnit && u.AIControlled == false)
                    {
						opponentsInRange.Add (u);
					}
				}
			}
		}
		return opponentsInRange;
	}

	

	public void AIMoveToNearestEnemy(Unit unitAI){
        Debug.Log("AI turn starts" + GM.BattleData.CurrentUnit.name);
		List<Unit> opponentsInRange = new List<Unit>();
		opponentsInRange = FindAllPlayerUnits ();
		//find nearest opponent
		Unit opponent = GM.Map.FindNearestEnemy (unitAI, opponentsInRange);
		Tile[] blocked = GM.Map.GetBlockedTiles();
		List<Tile> pathToOpponent = TilePathFinder.FindPath(unitAI.currentTile, opponent.currentTile, blocked, 100f);

		unitAI.currentPath = pathToOpponent;
		if (GM.Map.CalcPathCost (unitAI) > unitAI.MovementRange) {
			Debug.Log("Path reduce");
			int movementCost = 0;

			for(int i=0;i<pathToOpponent.Count;i++)
			{
				movementCost +=pathToOpponent[i].movementCost;
				if(movementCost > unitAI.MovementRange)
				{
					int countToRemove = pathToOpponent.Count - i;
					pathToOpponent.RemoveRange(i, countToRemove);
				}
			}
		} else {
			pathToOpponent.Remove (pathToOpponent [pathToOpponent.Count - 1]);
		}

		//TODO MoveUnit (unitAI);
	}

	



    public void OnDestroy()
    {
        InputController.Instance.OnTileClick -= TileClick;
        InputController.Instance.OnUnitClick -= SetCurrentUnit;
    }
}
