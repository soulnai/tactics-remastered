﻿using UnityEngine;
using System.Collections;
using EnumSpace;

public class Cost : MonoBehaviour {
    public unitAttributes CostAttribute = unitAttributes.AP;
    public ModType CostType = ModType.Change;
    public int Value = 0;

    private Ability _ability;
    void Awake()
    {
        _ability = GetComponent<Ability>();
        _ability.OnApply += ApplyCost;
    }

    private void ApplyCost()
    {
        BaseAttribute attr = _ability.Owner.GetAttribute(CostAttribute);
        
        //TODO move to the Math section all this calculations
        GameMath.ChangeAttribute(attr,CostType,Value);
    }

    public bool CanPayCost()
    {
        bool _canPay = true;

        BaseAttribute attr = _ability.Owner.GetAttribute(CostAttribute);

        switch (CostType)
        {
            case ModType.Sub:
                if (attr.Value - Value < 0)
                {
                    _canPay = false;
                }
                break;

                case ModType.Change:
                if (attr.Value == 0)
                {
                    _canPay = false;
                }
                break;
        }

        return _canPay;
    }
}
