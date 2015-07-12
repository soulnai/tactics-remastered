using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAbilitiesPanel : MonoBehaviour {
    public GameObject AbilityButtonPrefab;
    public Unit Unit;

    private List<AbilityButton> buttonsList; 
    public void Awake()
    {
        GM.Events.OnCurrentUnitChanged += Init;
        buttonsList = new List<AbilityButton>();
    }

    public void Init(Unit u)
    {
        Unit = u;
        if (Unit != null)
        {
            for (int i = 0; i < buttonsList.Count; i++)
            {
                AbilityButton a = buttonsList[i];
                Destroy(a.gameObject);
            }
            buttonsList.Clear();

            foreach (Ability a in Unit.Abilities)
            {
                GameObject go = Instantiate(AbilityButtonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                go.transform.SetParent(transform, false);
                go.GetComponent<AbilityButton>().Init(a);
                buttonsList.Add(go.GetComponent<AbilityButton>());
            }
        }
    }
}
