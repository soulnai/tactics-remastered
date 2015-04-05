using UnityEngine;
using System.Collections;
using EnumSpace;

public class GUIController : MonoBehaviour
{

    private static GUIController _instance;

    public GameObject LoadingScreen;
    
    private ScenesController _scenesController;
    public bool mouseOverGUI = false;

    public static GUIController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GUIController>();

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
        _scenesController = ScenesController.instance;
    }


    internal void SetButtonAction(ScenesEnum ActionToSet)
    {
        switch (ActionToSet)
        {
            case ScenesEnum.BattleScene:
                _scenesController.LoadScene("level");
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
