using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScenarioController : MonoBehaviour {
	public List <List<Tile>> map = new List<List<Tile>>();
	public int mapSize;
	private ScenarioController _ScenarioController;
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
		_ScenarioController = ScenarioController.instance;
		MapController.loadMapFromXml ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
