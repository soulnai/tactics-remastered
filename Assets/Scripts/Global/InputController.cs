using System;
using UnityEngine;
using System.Collections;
using EnumSpace;
using UnityEngine.Events;

/*-----------------------------------------------------

    Обрабатывает все управление, входящие клики, нажатия и пр.

-----------------------------------------------------*/
public class InputController : MonoBehaviour
{
    public Action<Tile> OnTileClick;
	public Action<Unit> OnUnitClick;
    public Action<Unit> OnUnitDropToSlot;
    public Action OnBattleLoad;

    private static InputController _instance;

    public static InputController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InputController>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    // Use this for initialization
    void Start()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
	
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
