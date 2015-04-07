﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;


/*-----------------------------------------------------

    Отвечает за описание и проверку целей и условий в даной сцене
     
-----------------------------------------------------*/

public class ScenarioController : MonoBehaviour {
	public List <List<Tile>> map = new List<List<Tile>>();
	public int mapSize;

	public UnitSpawn UnitSpawner;
	public SpawnMiscObjects ObjSpawner;
	public GameObject[] UnitPrefabsHolder;
	public GameObject[] MiscPrefabsHolder;

	private static ScenarioController _instance;
	public static ScenarioController instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<ScenarioController>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}
	public MapUtils MapController;
	// Use this for initialization
	void OnAwake(){
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

	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Init()
    {

        MapController.loadMapFromXml();
		UnitSpawner.spawnunit (map[9][9], 1);
		ObjSpawner.SpawnMapObject (map[8][8], 0);
		ObjSpawner.SpawnMapObject (map[2][2], 1);
		ObjSpawner.SpawnMapObject (map[12][12], 2);
		ObjSpawner.SpawnMapObject (map[8][4], 3);
		ObjSpawner.SpawnMapObject (map[1][12], 5);
		ObjSpawner.SpawnMapObject (map[2][12], 6);
		ObjSpawner.SpawnMapObject (map[6][6], 7);


    }
}
