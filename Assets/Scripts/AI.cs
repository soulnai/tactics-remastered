using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumSpace;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
                GM.BattleData.CurrentUnit = GM.BattleData.NextUnitWithAP;
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

            List<Unit> enemyUnits = GM.Map.getAllUnitsinAreaForAttack(unit.CurrentTile, 1);
            
            if (enemyUnits.Count > 0)
            {
                //TODO add range of available attacks check
                Debug.Log("ATTACK");
                AIAttack(unit, enemyUnits.OrderBy(x => x != null ? -x.HP.Value : 1000).First());
            }
            else if ((unit.CurrentAction == unitActions.idle) && (unit.AP.Value > 0))
            {
                List<Unit> enemyUnitsToMove = GM.BattleData.Players[0].SpawnedPartyUnits;
                if (enemyUnitsToMove.Count > 0)
                {
                    Debug.Log("Move1");
                    AIMoveToNearestEnemy(unit, enemyUnitsToMove);
                }
                else 
                {
                    Debug.Log("Move2");
                    enemyUnitsToMove = GM.BattleData.Players[0].SpawnedPartyUnits;
                    AIMoveToNearestEnemy(unit, enemyUnitsToMove);
                }
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

    public static void AIAttack(Unit AIunit, Unit target) 
    {
        if (AIunit.AP.Value > 0)
        {
            Animation anim = AIunit.gameObject.GetComponentInChildren<Animation>();
            
            if (GameMath.TryHit(AIunit, target))
            {
                int damage = GameMath.CalculateDamage(AIunit, target);
                GameMath.applyDamage(target, damage);
                Debug.Log("Damage was applied " + damage);
            }

            AIunit.ReduceAP();
            Debug.Log("AI unit - " + AIunit.UnitName + " - near enemy unit.Ready to attack");
            AIUnitTurnEnd(AIunit);
        }
        else 
        {
            AIUnitTurnEnd(AIunit);
        }
    }
    public static void AIMoveToNearestEnemy(Unit unitAI, List<Unit> opponents)
    {
        GM.Events.OnUnitActionChange += AILogic;
        Debug.Log("AI turn starts" + GM.BattleData.CurrentUnit.name);
        List<Unit> opponentsInRange = new List<Unit>();
        Debug.Log("------------>" + GM.BattleData.CurrentUnit);
        opponentsInRange = opponents;
        //find nearest opponent
        Unit opponent = GM.Map.FindNearestEnemy(unitAI, opponentsInRange);
        List<Tile> pathToOpponent = TilePathFinder.FindPath(unitAI.CurrentTile, opponent.CurrentTile, unitAI.MaxHeight);

        unitAI.CurrentPath = pathToOpponent;
        if (GM.Map.CalcPathCost(unitAI) > unitAI.MovementRange)
        {
            Debug.Log(pathToOpponent.Count());
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
