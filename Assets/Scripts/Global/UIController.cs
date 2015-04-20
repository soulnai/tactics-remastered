using System;
using UnityEngine;
using System.Collections;
using EnumSpace;

public class UIController : Singleton<UIController>
{
    protected UIController() { } // guarantee this will be always a singleton only - can't use the constructor!

    public GameObject LoadingScreen; 

    private ScenesController _scenesController;
    public bool mouseOverGUI = false;



    void Awake()
    {

    }

    void Start()
    {
        _scenesController = ScenesController.Instance;
    }


    public void SetButtonAction(ScenesEnum ActionToSet)
    {
        switch (ActionToSet)
        {
            case ScenesEnum.BattleScene:
                _scenesController.TryLoadBattle();
                break;
            case ScenesEnum.Tavern:
                _scenesController.LoadTavern();
                break;
            case ScenesEnum.mainMenu:
                _scenesController.LoadMainMenu();
                break;
        }
    }

    public void ShowLoadingScreen()
    {
        GameObject _loadingScreen = Instantiate(LoadingScreen) as GameObject;
        _loadingScreen.transform.SetParent(FindObjectOfType<Canvas>().transform,false);
    }


}
