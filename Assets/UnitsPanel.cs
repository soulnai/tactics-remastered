using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitsPanel : MonoBehaviour
{

    private GlobalGameController _globalGame;
    private List<GameObject> _slots;
    
	// Use this for initialization
	void Start () {
        _slots = new List<GameObject>();
        _globalGame = GlobalGameController.instance;
	    foreach (Transform child in transform)
	    {
	        if (child.GetComponent<UnitSlot>())
	        {
	            _slots.Add(child.gameObject);
	        }
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlaceUnitsInSlots(UnitListElement unitListElement)
    {
        int i = 0;
        foreach (Unit u in _globalGame.Player.AvailableUnits)
        {
            UnitListElement uElement = Instantiate(unitListElement, _slots[i].transform.position, Quaternion.identity) as UnitListElement;
            uElement.transform.SetParent(_slots[i].transform);
            uElement.Unit = u;
            i++;
        }
    }
}
