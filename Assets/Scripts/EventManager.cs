using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;
using EnumSpace;

public class EventManager : Singleton<EventManager>
{
    //player turn events
    public Action<Player> OnPlayerTurnEnd;
    public Action<Player> OnPlayerTurnStart;

    //unit turn events
    public Action<Unit> OnUnitTurnEnd;
    public Action<Unit> OnUnitTurnStart;
    public Action<Unit> OnUnitMoveComplete; 
    public Action<Unit, unitActions> OnUnitActionChange; 
    
    //change action state
    public Action<Unit, EnumSpace.unitActions> OnUnitStateChange;
    
    // receive ability
    public Action<List<Unit>, AbilityContainer> OnUnitAbilityReceived;

    //Selection
    public Action<Unit> OnCurrentUnitChanged;
 
    protected EventManager() { } // guarantee this will be always a singleton only - can't use the constructor!

    public void PlayerTurnEnded(Player player)
    {
        if (OnPlayerTurnEnd != null)
        {
            OnPlayerTurnEnd(player); 
            
        }
        Debug.Log("Player - " + player.PlayerName + " turn ended");
    }

    public void PlayerTurnStarted(Player player)
    {
        if (OnPlayerTurnStart != null)
        {
            OnPlayerTurnStart(player);
            
        }
        Debug.Log("Player - " + player.PlayerName + " turn started");
    }

    public void UnitTurnStarted(Unit unit)
    {
        if (OnUnitTurnStart != null)
        {
            OnUnitTurnStart(unit);
            
        }
        Debug.Log("Unit - " + unit.UnitName + " turn started");
    }

    public void UnitTurnEnded(Unit unit)
    {
        if (OnUnitTurnEnd != null)
        {
            OnUnitTurnEnd(unit);
            
        }
        Debug.Log("Unit - " + unit.UnitName + " turn ended");
    }

    public void CurrentUnitChanged(Unit unit)
    {
        if (OnCurrentUnitChanged != null)
        {
            OnCurrentUnitChanged(unit);

        }
        Debug.Log("Unit - " + unit.UnitName + " current unit changed");
    }

    public void UnitActionChanged(Unit unit, unitActions action)
    {
        if (OnUnitActionChange != null)
        {
            OnUnitActionChange(unit,action);
        }
        Debug.Log("Unit changed action - " + unit.UnitName + " to - " + action);
    }

    public void UnitMoveCompleted(Unit unit)
    {
        if (OnUnitMoveComplete != null)
        {
            OnUnitMoveComplete(unit);
        }
        Debug.Log("Unit completed movement - " + unit.UnitName);    
    }
}
