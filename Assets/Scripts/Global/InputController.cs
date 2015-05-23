using System;
using UnityEngine;
using System.Collections;
using EnumSpace;
using UnityEngine.Events;

/*-----------------------------------------------------

    Обрабатывает все управление, входящие клики, нажатия и пр.

-----------------------------------------------------*/
public class InputController : Singleton<InputController>
{
    protected InputController() { } // guarantee this will be always a singleton only - can't use the constructor!

    public Action<Tile> OnTileClick;
	public Action<Unit> OnUnitClick;
    public Action<Ability> OnAbilityClick;
    public Action<Unit> OnUnitDropToSlot;
    public Action OnBattleLoad;


    // Use this for initialization
    void Awake()
    {

    }

    public void OnAbilityClicked(Ability a)
    {
        if (OnAbilityClick != null)
            OnAbilityClick(a);
    }

    public void OnTileClicked(Tile t)
    {
        if (OnTileClick != null)
            OnTileClick(t);
    }

	public void OnUnitClicked(Unit u)
	{
		if (OnUnitClick != null)
			OnUnitClick(u);
	}

    public void OnUnitDroppedToSlot(Unit u)
    {
        if (OnUnitDropToSlot != null)
            OnUnitDropToSlot(u);
    }

    public void OnBattleLoadPressed()
    {
        if (OnBattleLoad != null)
        {
            OnBattleLoad();
        }
    }
}
