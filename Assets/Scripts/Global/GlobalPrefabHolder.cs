using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalPrefabHolder : Singleton<GlobalPrefabHolder>
{
    protected GlobalPrefabHolder() { } // guarantee this will be always a singleton only - can't use the constructor!

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


    void Awake() {
        LoadAllPrefabs();
    }


    //stores all prefabs in Prefabs
    public void LoadAllPrefabs()
    {
        Prefabs = new Dictionary<string, GameObject>();
        foreach (GameObject prefab in Resources.LoadAll("Level1/Prefabs", typeof(GameObject)))
        {
            Prefabs.Add(prefab.name,prefab);
        }
    }
}
