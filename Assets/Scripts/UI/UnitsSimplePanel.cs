using UnityEngine;
using System.Collections;

public class UnitsSimplePanel : MonoBehaviour {

    public Player player;
    public GameObject unitPanelPrefab;

	// Use this for initialization
	void Start ()
	{
	    player = GM.BattleData.Players[0];
        foreach (Unit unit in player.SpawnedPartyUnits)
        {
            GameObject go = Instantiate(unitPanelPrefab,Vector3.zero, Quaternion.identity) as GameObject;
            go.transform.SetParent(transform,false);
            go.GetComponent<UIWatchPanel>().UnitToWatch = unit;
        }
	}
}
