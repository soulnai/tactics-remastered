using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UnitSlot : MonoBehaviour, IDropHandler
{
    public GameObject item
    {
        get
        {
            if (transform.childCount>0)
            {
                return transform.GetChild(0).gameObject;
            }
            else
            {
                return null;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            UnitDragHandler.DraggedObj.transform.SetParent(transform,false);
            UnitDragHandler.DraggedObj.transform.localPosition = Vector3.zero;
        }
    }
}
