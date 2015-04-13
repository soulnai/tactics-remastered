using UnityEngine;
using System.Collections;

public class SpawnMiscObjects : MonoBehaviour {
	private ScenarioController gm;
	public static SpawnMiscObjects instance;
    private GlobalPrefabHolder _prefabHolder;

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
        _prefabHolder = GlobalPrefabHolder.instance;
        Instantiate(_prefabHolder.MiscPrefabsHolder[obj], tile.transform.position + new Vector3(0f, .5f, 0f), Quaternion.identity);
		tile.impassible = true;
	}
}
