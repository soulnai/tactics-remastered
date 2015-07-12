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
        InputController.Instance.OnTileClick += MoveToTile;
        InputController.Instance.OnUnitClick += SelectUnit;
        StartPlayerTurn(GM.BattleData.currentPlayer);
        AI.InitAI();
    }

    public void MainTurnLogic()
    {
        
    }

    public void StartPlayerTurn(Player player)
    {
        player.TurnState = playerTurnStates.start;
        GM.BattleData.currentPlayer = player;
        player.InitTurn();
        if(GM.BattleData.NextUnitWithAP != null)
            GM.BattleData.CurrentUnit = GM.BattleData.NextUnitWithAP;
        else
        {
            Debug.LogError("No Unit with AP on start of the turn");
        }
        GM.BattleData.UnitControlState = unitTurnStates.canInteract;
        GM.Events.PlayerTurnStarted(player);

        if (GM.BattleData.currentPlayer.UserControlled == false)
        {
            AI.AITurn(player);
        }
    }

    public void EndPlayerTurn(Player player)
    {
        if (GM.BattleData.UnitControlState == unitTurnStates.canInteract)
        {
            StartPlayerTurn(GM.BattleData.NextPlayer);
        }
    }

    public void SelectUnit(Unit unit)
    {
        if ((unit.OwnerPlayer == GM.BattleData.currentPlayer) &&
            (GM.BattleData.UnitControlState == unitTurnStates.canInteract) &&
            (!GM.UI.mouseOverGUI))
        {
            GM.BattleData.CurrentUnit = unit;
        }
    }

    public void MoveToTile(Tile tile)
    {
        if ((GM.BattleData.CurrentUnit.OwnerPlayer == GM.BattleData.currentPlayer) &&
            (GM.BattleData.UnitControlState == unitTurnStates.canInteract) && 
            (!GM.UI.mouseOverGUI))
        {
            GM.BattleData.CurrentUnit.MoveTo(tile);
        }
    }
}
