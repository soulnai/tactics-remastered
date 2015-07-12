using UnityEngine;
using System.Collections;

public class DelayAbility : MonoBehaviour {
    public int Value;
   
    private Ability _ability;
    void Awake()
    {
        _ability = GetComponent<Ability>();
    }
}
