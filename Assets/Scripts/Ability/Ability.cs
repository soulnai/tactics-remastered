using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour
{
    public string AbilityName;
    [HideInInspector]
    public bool Selected;
    
	// Use this for initialization
	void Start () {
        GM.Input.OnAbilityClick += SetSelected;
	}
	
	// Update is called once per frame
    void SetSelected(Ability a)
    {
        if (this == a)
        {
            Selected = true;
        }
        else
            Selected = false;
    }
}
