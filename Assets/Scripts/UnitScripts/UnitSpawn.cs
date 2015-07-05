using UnityEngine;
using System.Collections;

public class UnitSpawn : MonoBehaviour {

	// Use this for initialization
	void OnAwake(){

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Unit SpawnUnit(Tile tile, GameObject unitPrefab, Player ownerPlayer){
	    GameObject go =
	        ((GameObject)
	            Instantiate(unitPrefab,
                tile.transform.position,
	            Quaternion.identity));
        Unit unit = go.GetComponent<Unit>();
		unit.CurrentTile = tile;
	    unit.OwnerPlayer = ownerPlayer;
        return unit;
	}
}
