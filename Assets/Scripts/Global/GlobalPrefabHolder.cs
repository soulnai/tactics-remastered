using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalPrefabHolder : MonoBehaviour {

	public GameObject BASE_TILE_PREFAB;

	public GameObject TILE_NORMAL_PREFAB;
	public GameObject TILE_DIFFICULT_PREFAB;
	public GameObject TILE_VERY_DIFFICULT_PREFAB;
	public GameObject TILE_IMPASSIBLE_PREFAB;
    //Units
    public GameObject[] UnitPrefabsHolder;
    //MapStuff
    public GameObject[] MiscPrefabsHolder;

	public GameObject player;

    public Dictionary<string,GameObject> Prefabs; 

    private static GlobalPrefabHolder _instance;

    public static GlobalPrefabHolder instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GlobalPrefabHolder>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake() {
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

        LoadAllPrefabs();
    }


    //stores all prefabs in Prefabs
    public void LoadAllPrefabs()
    {
        Prefabs = new Dictionary<string, GameObject>();
        foreach (GameObject prefab in Resources.LoadAll("Prefabs", typeof(GameObject)))
        {
            Prefabs.Add(prefab.name,prefab);
        }
    }
}
