using UnityEngine;
using System.Collections;

public class ScenesController : MonoBehaviour {

    private static ScenesController _instance;
    public string sceneMainMenuName = "MainMenu";
    public string sceneTavernName = "Tavern";

    private GUIController _guiController;

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
        _guiController = GUIController.instance;
    }

    public void LoadScene(string name) {
        _guiController.ShowLoadingScreen();
        Application.LoadLevel(name);
    }

    public void LoadMainMenu()
    {
        _guiController.ShowLoadingScreen();
        Application.LoadLevel(sceneMainMenuName);
    }
    public void LoadTavern()
    {
        _guiController.ShowLoadingScreen();
        Application.LoadLevel(sceneTavernName);
    }

}
