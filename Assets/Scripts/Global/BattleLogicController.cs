using System.Collections.Generic;
using UnityEngine;

//using UnityEditor;

/*-----------------------------------------------------

    Логика боя, работа с юнитами
     
-----------------------------------------------------*/

public class BattleLogicController : Singleton<BattleLogicController>
{
    protected BattleLogicController() { } // guarantee this will be always a singleton only - can't use the constructor!



    public void Init()
    {
        InputController.Instance.OnTileClick += TileClick;
        InputController.Instance.OnUnitClick += GM.BattleData.SetCurrentUnit;

        StartPlayerTurn(GM.BattleData.currentPlayer);
    }

    public void TileClick(Tile tile)
    {
        GM.BattleData.CurrentUnit.MoveTo(tile);
    }

    public void StartPlayerTurn(Player player)
    {
        GM.BattleData.currentPlayer = player;
        GM.Events.PlayerTurnStarted(player);
        StartUnitTurn(GM.BattleData.currentPlayer.SpawnedPartyUnits[0]);
    }

    public void EndPlayerTurn()
    {
        GM.Events.PlayerTurnEnded(GM.BattleData.currentPlayer);
        StartPlayerTurn(GM.BattleData.NextPlayer);
    }

    public void StartUnitTurn(Unit unit)
    {
        GM.BattleData.CurrentUnit = unit;
        GM.Events.UnitTurnStarted(unit);
    }

    public void EndUnitTurn(Unit unit = null)
    {
        if (unit != null)
        {
            GM.Events.UnitTurnEnded(unit);
        }
        else
        {
            GM.Events.UnitTurnEnded(GM.BattleData.CurrentUnit);
        }
        StartUnitTurn(GM.BattleData.NextUnit);
    }

	public void AITurn(Unit unitAI){
		AIMoveToNearestEnemy (unitAI);
	}

	public void AIMoveToNearestEnemy(Unit unitAI){
        Debug.Log("AI turn starts" + GM.BattleData.CurrentUnit.name);
		List<Unit> opponentsInRange = new List<Unit>();
		
		//find nearest opponent
		Unit opponent = GM.Map.FindNearestEnemy (unitAI, opponentsInRange);
		Tile[] blocked = GM.Map.GetBlockedTiles();
		List<Tile> pathToOpponent = TilePathFinder.FindPath(unitAI.currentTile, opponent.currentTile, blocked, 100f);

		unitAI.currentPath = pathToOpponent;
	    if (GM.Map.CalcPathCost(unitAI) > unitAI.MovementRange)
	    {
	        Debug.Log("Path reduce");
	        int movementCost = 0;

	        for (int i = 0; i < pathToOpponent.Count; i++)
	        {
	            movementCost += pathToOpponent[i].movementCost;
	            if (movementCost > unitAI.MovementRange)
	            {
	                int countToRemove = pathToOpponent.Count - i;
	                pathToOpponent.RemoveRange(i, countToRemove);
	            }
	        }
	    }
	    else
	    {
	        pathToOpponent.Remove(pathToOpponent[pathToOpponent.Count - 1]);
	    }

	    //TODO MoveUnit (unitAI);
	}
}
