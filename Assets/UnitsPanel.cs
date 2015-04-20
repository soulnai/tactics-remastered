using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumSpace;
using UnityEngine.EventSystems;

public class UnitsPanel : MonoBehaviour, IDropHandler
{
    public GameObject unitListItemPrefab;
    public EnumSpace.UnitListPanelType PanelType;
    private GlobalGameController _globalGame;
    private List<UnitSlot> _slots;

    public UnitSlot FirstEmptySlot
    {
        get
        {
            foreach (UnitSlot slot in _slots)
            {
                if (slot.UnitItem == null)
                {
                    return slot;
                }
            }
            return null;
        }
    }


    // Use this for initialization
    void Start () {
        _slots = new List<UnitSlot>();
        _globalGame = GlobalGameController.Instance;
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

    public void PlaceUnitsInSlots(List<Unit> unitsList)
    {
        int i = 0;
        foreach (Unit u in unitsList)
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

    public void OnDrop(PointerEventData eventData)
    {
        GameObject tempItem = eventData.pointerDrag;
        if (FirstEmptySlot)
        {
            tempItem.transform.SetParent(FirstEmptySlot.transform, false);
            tempItem.transform.localPosition = Vector3.zero;
            InputController.Instance.OnUnitDroppedToSlot(tempItem.GetComponent<Unit>());
        }
    }
}
