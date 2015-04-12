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
		Instantiate(gm.MiscPrefabsHolder[obj], tile.transform.position + new Vector3(0f,.5f,0f), Quaternion.identity);
		tile.impassible = true;
	}
}
