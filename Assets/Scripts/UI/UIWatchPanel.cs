using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIWatchPanel : MonoBehaviour {
    
    public Player PlayerToWatch;
    public Unit UnitToWatch;

    public List<UIWatchElement> WatchElements;
 
    // Use this for initialization
	void Start () {
	    foreach (Transform child in transform)
	    {
	        if (child.GetComponent<UIWatchElement>() != null)
	        {
                WatchElements.Add(child.GetComponent<UIWatchElement>());
	        }
	    }

	    foreach (UIWatchElement element in WatchElements)
	    {
	        element.Init(PlayerToWatch,UnitToWatch);
	    }
	}
}
