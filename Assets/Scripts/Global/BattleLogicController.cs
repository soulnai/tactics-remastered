using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;
//using UnityEditor;

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

    public void PlayerTurnEnded(Player player)
    {
        if (OnPlayerTurnEnd != null) {OnPlayerTurnEnd(player);}
    }

    public void PlayerTurnStarted(Player player)
    {
        if (OnPlayerTurnStart != null) { OnPlayerTurnStart(player); }
    }

    public void UnitTurnStarted(Unit unit)
    {
        if (OnUnitTurnStart != null) { OnUnitTurnStart(unit); }
    }

    public void UnitTurnEnded(Unit unit)
    {
        if (OnUnitTurnEnd != null) { OnUnitTurnEnd(unit); }
    }
 

    public void Init()
    {
        InputController.Instance.OnTileClick += TileClick;
		InputController.Instance.OnUnitClick += SetCurrentUnit;

        GM.BattleData.currentPlayer = GM.BattleData.Players[0];
        GM.BattleData.CurrentUnit = GM.BattleData.Players[0].SpawnedPartyUnits[0];
    }

    public void TileClick(Tile tile)
    {
        GM.BattleData.CurrentUnit.MoveTo(tile);
    }

    public void SetCurrentUnit(Unit unit){
		GM.BattleData.CurrentUnit = unit;
	}

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

    public void StartPlayerTurn()
    {
        PlayerTurnStarted(GM.BattleData.currentPlayer);
    }

    public void EndPlayerTurn()
    {
        PlayerTurnEnded(GM.BattleData.currentPlayer);
        NextPlayer();
    }

    public void StartUnitTurn()
    {
        UnitTurnStarted(GM.BattleData.CurrentUnit);
    }

    public void EndUnitTurn()
    {
        UnitTurnEnded(GM.BattleData.CurrentUnit);
        NextUnit();
    }
    public void UnitTurnLogic()
    {
        UnitTurnStarted(GM.BattleData.CurrentUnit);
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
		}
	}

	public void NextPlayer () {
	    if (GM.BattleData.Players.FindIndex(x => x == GM.BattleData.currentPlayer) < GM.BattleData.Players.Count - 1)
	    {
            GM.BattleData.currentPlayer = GM.BattleData.Players[GM.BattleData.Players.FindIndex(x => x == GM.BattleData.currentPlayer) + 1];
	    }
	    else
	    {
	        GM.BattleData.currentPlayer = GM.BattleData.Players[0];
	    }

        PlayerTurnStarted(GM.BattleData.currentPlayer);
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

    public int CalculateDamage(Unit attacker, Unit defender) 
    {
        if (CheckHit(attacker, defender)) 
        {
            if (!CheckEvade(attacker, defender)) 
            {
                if (CheckCrit(attacker, defender))
                {
                    int CritDamageToApply = (int)(((UnityEngine.Random.RandomRange(((float)attacker.MinCurrentWeaponAtk, (float)attacker.MaxCurrentWeaponAtk)+attacker.Strength/2)-(float)defender.PhysicalDef)*attacker.CritMultiplier);
                    if (CritDamageToApply<=0)
                    {
                        return 1;
                    } else 
                    {
                        return CritDamageToApply;
                    }
                }
                else {
                    int DamageToApply = (int)((UnityEngine.Random.RandomRange(((float)attacker.MinCurrentWeaponAtk, (float)attacker.MaxCurrentWeaponAtk)+attacker.Strength/2)-(float)defender.PhysicalDef);
                    if (DamageToApply<=0)
                    {
                        return 1;
                    } else 
                    {
                        return DamageToApply;
                    }   
                }
            }
        }
        return 0;
    }

    public bool CheckHit(Unit attacker, Unit defender) 
    {
        if (UnityEngine.Random.value <= attacker.ToHitChance)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public bool CheckEvade(Unit attacker, Unit defender)
    {
        if (UnityEngine.Random.value <= attacker.EvadeChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckCrit(Unit attacker, Unit defender)
    {
        if (UnityEngine.Random.value <= attacker.CritChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
