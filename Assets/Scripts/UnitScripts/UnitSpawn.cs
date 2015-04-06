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

	public void spawnunit(Tile tile){
		gm = ScenarioController.instance;
		Unit unit = ((GameObject)Instantiate(gm.UnitPrefabsHolder[0], new Vector3(tile.transform.position.x, tile.transform.position.y + 1f, tile.transform.position.z), Quaternion.identity)).GetComponent<Unit>();
		unit.currentTile = tile;
		gm.unitsAll.Add(unit);
		gm.curentUnit = unit;
	}
}
