using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public Image Icon;
    public Ability Ability;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init(Ability a)
    {
        Ability = a;
        Icon.sprite = a.Icon;
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GM.Input.AbilityClicked(a);
            //GM.Events.AbilitySelected(a);
        });
    }
}
