using UnityEngine;
using System.Collections;

public class UnitSpawn : MonoBehaviour {
	private ScenarioController gm;
	public static UnitSpawn instance;

	// Use this for initialization
	void OnAwake(){
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Unit spawnunit(Tile tile, GameObject unitPrefab){
		gm = ScenarioController.instance;
	    GameObject go =
	        ((GameObject)
	            Instantiate(unitPrefab,
	                new Vector3(tile.transform.position.x, tile.transform.position.y + 0.5f, tile.transform.position.z),
	                Quaternion.identity));
        Unit unit = go.GetComponent<Unit>();
		unit.currentTile = tile;
        BattleDataController.instance.currentUnit = unit;
		return unit;
	}
}
