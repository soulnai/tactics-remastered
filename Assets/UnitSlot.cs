using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UnitSlot : MonoBehaviour, IDropHandler
{
    public UnitsPanel OwnerUnitsPanel;
    public UnitListItem UnitItem
    {
        get
        {
            if (transform.childCount>0)
            {
                return transform.GetChild(0).GetComponent<UnitListItem>();
            }
            else
            {
                return null;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!UnitItem)
        {
            UnitDragHandler.DraggedObj.transform.SetParent(transform,false);
            UnitDragHandler.DraggedObj.transform.localPosition = Vector3.zero;
            InputController.instance.OnUnitDroppedToSlot(UnitItem.Unit);
        }
    }
}
