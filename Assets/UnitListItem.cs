using UnityEngine;
using System.Collections;

public class UnitListItem : MonoBehaviour
{

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
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
