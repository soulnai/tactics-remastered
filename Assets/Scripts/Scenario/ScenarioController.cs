using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScenarioController : MonoBehaviour {
	public List <List<Tile>> map = new List<List<Tile>>();
	public int mapSize;
	public static ScenarioController instance;
	public MapUtils MapController;
	// Use this for initialization
	void OnAwake(){

	}

	void Start () {
		MapController.loadMapFromXml ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
