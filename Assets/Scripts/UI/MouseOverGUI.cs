﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseOverGUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool UseOverUIFlag = true;
    private UIController _uiController;

	// Use this for initialization
	void Start ()
	{
	    _uiController = UIController.Instance;
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
