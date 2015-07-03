using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumSpace;

public static class AI  {

    public static void InitAI()
    {
        //GM.Events.OnPlayerTurnStart += AITurn;
    }

    public static void AITurn(Player player)
    {
        if (GM.BattleData.currentPlayer.UserControlled == false)
        {
            if (GM.BattleData.NextUnitWithAP != null)
            {
                AILogic(GM.BattleData.NextUnitWithAP);
            }
            else
            {
                GM.BattleLogic.StartPlayerTurn(GM.BattleData.NextPlayer);
            }
        }
    }


    public static void AILogic(Unit unit, unitActions prev, unitActions current)
    {
        AILogic(unit);
    }

    public static void AILogic(Unit unit)
    {
        if ((unit != null) && (unit.OwnerPlayer.UserControlled == false) && (unit.OwnerPlayer == GM.BattleData.currentPlayer))
        {
            GM.Events.OnUnitActionChange -= AILogic;

            List<Unit> enemyUnits = GM.Map.getAllUnitsinArea(unit.currentTile, 1);
            
            if (enemyUnits.Count > 0)
            {
                int damage = GameMath.CalculateDamage(unit, enemyUnits[0]);
                GameMath.applyDamage(enemyUnits[0], damage);
                Debug.Log("Damage was applied " + damage);
                unit.ReduceAP();
                Debug.Log("AI unit - " + unit.UnitName + " - near enemy unit.Ready to attack");
                AIUnitTurnEnd(unit);
            }
            else if ((unit.CurrentAction == unitActions.idle) && (unit.AP > 0))
            {
                AIMoveToNearestEnemy(unit);
            }
            else
            {
                AIUnitTurnEnd(unit);
            }
        }
        else
        {
            AIUnitTurnEnd(unit);
        }
    }

    private static void AIUnitTurnEnd(Unit unit)
    {
        if(unit != null)
        AITurn(GM.BattleData.currentPlayer);
    }

    public static void AIMoveToNearestEnemy(Unit unitAI)
    {
        GM.Events.OnUnitActionChange += AILogic;
        Debug.Log("AI turn starts" + GM.BattleData.CurrentUnit.name);
        List<Unit> opponentsInRange = new List<Unit>();
        Debug.Log("------------>" + GM.BattleData.CurrentUnit);
        opponentsInRange = MapUtils.Instance.getAllUnitsinArea(unitAI.currentTile, unitAI.MovementRange * 2);
        //find nearest opponent
        Unit opponent = GM.Map.FindNearestEnemy(unitAI, opponentsInRange);
        Tile[] blocked = GM.Map.BlockedTiles();
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
        unitAI.Move();
    }
}
