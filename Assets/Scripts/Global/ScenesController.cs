using System;
using UnityEngine;
using System.Collections;
using EnumSpace;

/*-----------------------------------------------------

    Загрузка сцен
     
-----------------------------------------------------*/

public class ScenesController : Singleton<ScenesController>
{
    protected ScenesController() { } // guarantee this will be always a singleton only - can't use the constructor!

    public Action OnBattleSceneLoadingStart;
    public Action<ScenesEnum> OnLevelLoadComplete;

    public string sceneMainMenuName = "MainMenu";
    public string sceneTavernName = "Tavern";
    public string sceneBattleName = "Level";

    private UIController _uiController;
    private BattleDataController _battleData;

    void Awake()
    {

    }

    void Start()
    {
        _battleData = BattleDataController.Instance;
        _uiController = UIController.Instance;
    }

    public void LoadScene(string name) {
        _uiController.ShowLoadingScreen();
        Application.LoadLevel(name);
    }

    public void LoadMainMenu()
    {
        _uiController.ShowLoadingScreen();
        Application.LoadLevel(sceneMainMenuName);
    }

    public void LoadEditor()
    {
        _uiController.ShowLoadingScreen();
        Application.LoadLevel("MapCreatorScene");
    }
    public void LoadTavern()
    {
        _uiController.ShowLoadingScreen();
        Application.LoadLevel(sceneTavernName);
    }

    //prepare all data to be ready for scene to load
    public void TryLoadBattle()
    {
        OnBattleSceneLoadingStarted();
        if (_battleData.ReadyToStart)
        {
            _uiController.ShowLoadingScreen();
            Application.LoadLevel(sceneBattleName);
        }
        else
        {
            Debug.Log("Battle Data not ready");
        }
    }

    public void OnLevelWasLoaded(int levelInt)
    {
        if (OnLevelLoadComplete != null)
        {
            OnLevelLoadComplete((ScenesEnum)levelInt);
        }
    }

    public void OnBattleSceneLoadingStarted()
    {
        if (OnBattleSceneLoadingStart != null)
        {
            OnBattleSceneLoadingStart();
        }
    }
}
