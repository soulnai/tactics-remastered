using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAbilitiesHolder : MonoBehaviour
{
    public List<Ability> Abilities;
    
    // Use this for initialization
	void Start () {
        Abilities = new List<Ability>();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Ability>() != null)
                Abilities.Add(child.GetComponent<Ability>());
        }
	}

    public Ability GetByName(string name)
    {
        return Abilities.Find(ab => ab.AbilityName == name);
    }

    public bool Contains(Ability ab)
    {
        if (Abilities.Contains(ab))
            return true;
        else
            return false;
    }
}
