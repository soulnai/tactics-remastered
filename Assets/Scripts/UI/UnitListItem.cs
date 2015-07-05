using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitListItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler
{
    public Image IconImage;
    public Text NameText;
    public Text ClassText;

    public static GameObject DraggedObj;
    public Transform StartParent
    {
        get { return _startParent; }
    }

    private Unit _unit;
    public Unit Unit
    {
        get { return _unit; }
        set
        {
            _unit = value;
            Init();
        }
    }

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
    private PreBattleSceneController _preBattleSceneController;

    public void Start()
    {
        _preBattleSceneController = PreBattleSceneController.Instance;
        _startParent = transform.parent;
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
        if ((transform.parent == _startParent) || (transform.parent.GetComponent<Canvas>()))
        {
            transform.position = _startPosition;
        }
    }

    public void Init()
    {
        if (Unit)
        {
            IconImage.sprite = Unit.Icon;
            NameText.text = Unit.UnitName;
            ClassText.text = Unit.UnitClass.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (StartParent.parent == _preBattleSceneController.AvailableUnitsPanel.transform)
        {
            transform.SetParent(_preBattleSceneController.PartyUnitsPanel.FirstEmptySlot.transform,false);
        }
        else
        {
            transform.SetParent(_preBattleSceneController.AvailableUnitsPanel.FirstEmptySlot.transform, false);
        }
        _startParent = transform.parent;
        InputController.Instance.OnUnitDroppedToSlot(Unit);
    }
}
