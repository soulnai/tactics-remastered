using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UnitDragHandler : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
{

    public static GameObject DraggedObj;
    private Vector3 _startPosition;
    private Transform _startParent;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
        if ((transform.parent == _startParent)||(transform.parent.GetComponent<Canvas>()))
        {
            transform.position = _startPosition;
        }
    }
}
