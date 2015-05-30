using UnityEngine;
using System.Collections;

public class VisualController : MonoBehaviour {

    private Ability _ability;
    void Awake()
    {
        _ability = GetComponent<Ability>();
    }
}
