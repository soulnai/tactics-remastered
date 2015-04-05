using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseOverGUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool UseOverUIFlag;
    private GUIController _guiController;

	// Use this for initialization
	void Start ()
	{
	    _guiController = GUIController.instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UseOverUIFlag)
        {
            _guiController.mouseOverGUI = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (useGUILayout)
        {
            _guiController.mouseOverGUI = false;
        }
    }
}
