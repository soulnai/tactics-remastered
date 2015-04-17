using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UnitListItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public static GameObject DraggedObj;
    public Transform StartParent
    {
        get { return _startParent; }
    }

    public Unit Unit;

    public UnitSlot Slot
    {
        get
        {
            if (transform.GetComponentInParent<UnitSlot>())
            {
                return transform.GetComponentInParent<UnitSlot>();
            }
            return null;
        }
    }

    private Vector3 _startPosition;
    private Transform _startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        DraggedObj = this.gameObject;
        if (DraggedObj.GetComponent<UnitListItem>().Slot)
        {
            DraggedObj.GetComponent<UnitListItem>().Slot.UnitItem = null;
        }
        _startPosition = transform.position;
        _startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        transform.SetParent(FindObjectOfType<Canvas>().transform);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        DraggedObj = null;
        if ((transform.parent == _startParent) || (transform.parent.GetComponent<Canvas>()))
        {
            transform.position = _startPosition;
        }
    }
}
