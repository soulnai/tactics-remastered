using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseOverGUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool UseOverUIFlag;
    private UIController _uiController;

	// Use this for initialization
	void Start ()
	{
	    _uiController = UIController.instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UseOverUIFlag)
        {
            _uiController.mouseOverGUI = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (useGUILayout)
        {
            _uiController.mouseOverGUI = false;
        }
    }
}
