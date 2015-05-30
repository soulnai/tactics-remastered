using UnityEngine;
using System.Collections;
using EnumSpace;

public class Cost : MonoBehaviour {
    public unitAttributes CostAttribute;
    public ModType CostType;
    public int Value;

    private Ability _ability;
    void Awake()
    {
        _ability = GetComponent<Ability>();
    }
}
