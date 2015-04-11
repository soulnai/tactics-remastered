using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
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
        _startPosition = transform.position;
        _startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        DraggedObj = null;
        if (transform.parent == _startParent)
        {
            transform.position = _startPosition;
        }
    }
}
