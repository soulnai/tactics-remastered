using System.Collections.Generic;
using EnumSpace;
using UnityEngine;
using UnityEngine.Assertions.Must;

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
        InputController.Instance.OnUnitClick += UnitClick;
        
        GM.Events.OnPlayerTurnStart += AI.AITurn;

        StartPlayerTurn(GM.BattleData.currentPlayer);

    }

    public void UnitClick(Unit unit)
    {
        if (unit != null)
        {
          //  if (unit.OwnerPlayer == GM.BattleData.currentPlayer)
            {
                GM.BattleData.CurrentUnit = unit;
            }
        }
    }

    public void TileClick(Tile tile)
    {
        GM.BattleData.CurrentUnit.MoveTo(tile);
    }

    public void StartPlayerTurn(Player player)
    {
        GM.BattleData.currentPlayer = player;
        GM.Events.PlayerTurnStarted(player);
        if (GM.BattleData.currentPlayer.SpawnedPartyUnits.Count > 0)
            StartUnitTurn(GM.BattleData.currentPlayer.SpawnedPartyUnits[0]);
    }

    public void EndPlayerTurn(Player player)
    {
        GM.Events.PlayerTurnEnded(player);
        StartPlayerTurn(GM.BattleData.NextPlayer);
    }

    public void StartUnitTurn(Unit unit)
    {
        if (unit != null)
        {
            GM.BattleData.CurrentUnit = unit;
        }
        GM.Events.UnitTurnStarted(unit);
    }

    public void EndUnitTurn(Unit unit = null)
    {
        if (unit != null)
        {
            GM.Events.UnitTurnEnded(unit);
        }
    }

    

	
}
