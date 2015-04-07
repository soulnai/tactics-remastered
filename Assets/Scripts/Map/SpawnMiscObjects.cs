using UnityEngine;
using System.Collections;

public class SpawnMiscObjects : MonoBehaviour {
	private ScenarioController gm;
	public static SpawnMiscObjects instance;

	void OnAwake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnMapObject(Tile tile, int obj){
		gm = ScenarioController.instance;
		Instantiate(gm.MiscPrefabsHolder[obj], new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z), Quaternion.Euler(-90, 90, 0));
		tile.impassible = true;
	}
}
