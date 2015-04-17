using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UnitSlot : MonoBehaviour, IDropHandler
{
    public UnitsPanel OwnerUnitsPanel;
    private UnitListItem _unitItem;
    public UnitListItem UnitItem
    {
        get
        {
            _unitItem = null;

            if (transform.childCount > 0)
            {
                _unitItem = transform.GetChild(0).GetComponent<UnitListItem>();
            }
        return _unitItem;
        }
        set { _unitItem = value; }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragObj = eventData.pointerDrag;
        if (UnitItem)
        {
            UnitItem.transform.SetParent(dragObj.GetComponent<UnitListItem>().StartParent,false);
        }
        dragObj.transform.SetParent(transform, false);
        dragObj.transform.localPosition = Vector3.zero;
        InputController.instance.OnUnitDroppedToSlot(dragObj.GetComponent<Unit>());
    }
}
