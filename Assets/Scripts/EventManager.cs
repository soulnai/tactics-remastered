using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

public class EventManager : Singleton<EventManager>
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
}
