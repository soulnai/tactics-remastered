using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumSpace;

public class UnitsPanel : MonoBehaviour
{
    public EnumSpace.UnitListPanelType PanelType;
    private GlobalGameController _globalGame;
    private List<UnitSlot> _slots;
    
	// Use this for initialization
	void Start () {
        _slots = new List<UnitSlot>();
        _globalGame = GlobalGameController.instance;
	    foreach (Transform child in transform)
	    {
	        if (child.GetComponent<UnitSlot>())
	        {
	            _slots.Add(child.gameObject.GetComponent<UnitSlot>());
	        }
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlaceUnitsInSlots(GameObject unitListItemPrefab)
    {
        int i = 0;
        foreach (Unit u in _globalGame.UserPlayer.AvailableUnits)
        {
            GameObject tempItem = (GameObject)Instantiate(unitListItemPrefab, _slots[i].transform.position, Quaternion.identity);
            UnitListItem uItem = tempItem.GetComponent<UnitListItem>();
            uItem.transform.SetParent(_slots[i].transform);
            uItem.Unit = u;
            i++;
        }
    }

    public void UpdateUnitsList(Unit u)
    {
        List<Unit> tempList = new List<Unit>();
        foreach (UnitSlot slot in _slots)
        {
            if (slot.UnitItem != null)
            {
                tempList.Add(slot.UnitItem.Unit);
            }
        }
        switch (PanelType)
        {
                case UnitListPanelType.AvailableUnitsPanel:
                    _globalGame.UserPlayer.AvailableUnits = tempList;
                break;

                case UnitListPanelType.PartyUnitsPanel:
                    _globalGame.UserPlayer.PartyUnits = tempList;
                break;
        }
    }
}
