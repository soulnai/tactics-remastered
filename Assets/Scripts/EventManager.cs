using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;
using EnumSpace;
using UnityEngine.UI;

public class EventManager : Singleton<EventManager>
{
    //player turn events
    public Action<Player> OnPlayerTurnEnd;
    public Action<Player> OnPlayerTurnStart;

    //unit turn events
    public Action<Unit> OnUnitTurnEnd;
    public Action<Unit> OnUnitTurnStart;
    public Action<Unit> OnUnitActionComplete; 
    public Action<Unit, unitActions,unitActions> OnUnitActionChange;
    public Action<BaseAttribute, float, float> OnUnitAttributeChange;
    
    //change action state
    public Action<Unit, EnumSpace.unitActions> OnUnitStateChange;
    
    //  ability
    public Action<Ability> OnAbilitySelect; 

    //Selection
    public Action<Unit> OnCurrentUnitChanged;
 
    protected EventManager() { } // guarantee this will be always a singleton only - can't use the constructor!

    public void AbilitySelected(Ability a)
    {
        if (OnAbilitySelect != null)
        {
            OnAbilitySelect(a);
        }
    }

    public void UnitAttributeChanged(BaseAttribute attribute, float prevVal, float currVal)
    {
        if (OnUnitAttributeChange != null)
        {
            OnUnitAttributeChange(attribute,prevVal,currVal);  

        }
        Debug.Log(attribute.name + " - changed: "+prevVal + " -> "+currVal);
    }

    public void PlayerTurnEnded(Player player)
    {
        if (OnPlayerTurnEnd != null)
        {
            OnPlayerTurnEnd(player); 
            
        }
        //    Text Log = GameObject.Find("LogText").GetComponent<Text>();
        //    Log.text = Log.text + "<color=black>Player - " + player.PlayerName + " turn ended</color> \n";
        Debug.Log("Player - " + player.PlayerName + " turn ended");
    }

    public void PlayerTurnStarted(Player player)
    {
        if (OnPlayerTurnStart != null)
        {
            OnPlayerTurnStart(player);
            
        }
    //   Text Log = GameObject.Find("LogText").GetComponent<Text>();
    //   Log.text = Log.text + "<color=black>Player - " + player.PlayerName + " turn started</color> \n";
        Debug.Log("Player - " + player.PlayerName + " turn started");
    }

    public void UnitTurnStarted(Unit unit)
    {
        if (OnUnitTurnStart != null)
        {
            OnUnitTurnStart(unit);
            
        }
        Debug.Log(unit.OwnerPlayer + " - Unit - " + unit.UnitName + " turn started");
    }

    public void UnitTurnEnded(Unit unit)
    {
        if (OnUnitTurnEnd != null)
        {
            OnUnitTurnEnd(unit);
            
        }
        Debug.Log(unit.OwnerPlayer +  " - Unit - " + unit.UnitName + " turn ended");
    }

    public void CurrentUnitChanged(Unit unit)
    {
        if (OnCurrentUnitChanged != null)
        {
            OnCurrentUnitChanged(unit);

        }
        Debug.Log(unit.OwnerPlayer +  " - Unit - " + unit.UnitName + " current unit changed");
    }

    public void UnitActionChanged(Unit unit, unitActions prevAction,unitActions currentAction)
    {
        if (OnUnitActionChange != null)
        {
            OnUnitActionChange(unit, prevAction, currentAction);
        }
        Debug.Log(unit.OwnerPlayer + " - Unit changed action - " + unit.UnitName + " - " + prevAction + "->" + currentAction);
    }

    public void UnitActionCompleted(Unit unit)
    {
        if (OnUnitActionComplete != null)
        {
            OnUnitActionComplete(unit);
        }
        Debug.Log(unit.OwnerPlayer + " - Unit completed ACTION - " + unit.UnitName);    
    }
}
