using System;
using UnityEngine;
using System.Collections;

/*-----------------------------------------------------

    Загрузка сцен
     
-----------------------------------------------------*/

public class ScenesController : MonoBehaviour {

    private static ScenesController _instance;

    public Action OnBattleSceneLoadingStart;

    public string sceneMainMenuName = "MainMenu";
    public string sceneTavernName = "Tavern";
    public string sceneBattleName = "Level";

    private UIController _uiController;
    private BattleDataController _battleData;

    public static ScenesController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ScenesController>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    void Start()
    {
        _battleData = BattleDataController.instance;
        _uiController = UIController.instance;
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

    public void OnBattleSceneLoadingStarted()
    {
        if (OnBattleSceneLoadingStart != null)
        {
            OnBattleSceneLoadingStart();
        }
    }
}
