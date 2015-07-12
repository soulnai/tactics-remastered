using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using EnumSpace;

public static class GameMath {
    //TODO: add case for attack type to choose defence type
    public static int CalculateDamage(Unit attacker, Unit defender)
    {
        int MainStat = 0;

        switch(attacker.UnitClass)
        {
            case EnumSpace.unitClass.knigth:
                MainStat = attacker.Strenght.Value;
                break;
            case EnumSpace.unitClass.warrior:
                MainStat = attacker.Strenght.Value;
                break;
            case EnumSpace.unitClass.archer:
                MainStat = attacker.Dexterity.Value;
                break;
            case EnumSpace.unitClass.mage:
                MainStat = attacker.Magic.Value;
                break;
            case EnumSpace.unitClass.squire:
                MainStat = attacker.Dexterity.Value;
                break;
            case EnumSpace.unitClass.none:
                MainStat = attacker.Strenght.Value;
                break;
        }


        if (checkHeight(attacker, defender))
        {
            Text Log = GameObject.Find("LogText").GetComponent<Text>();
            if (CheckHit(attacker, defender))
            {
                if (!CheckEvade(attacker, defender))
                {
                    if (CheckCrit(attacker, defender))
                    {
                        int CritDamageToApply = (int)(((Random.Range(attacker.MinCurrentWeaponAtk, attacker.MaxCurrentWeaponAtk) + MainStat / 2) - (float)defender.PhysicalDef) * attacker.CritMultiplier);
                        if (CritDamageToApply <= 0)
                        {
                            Log = GameObject.Find("LogText").GetComponent<Text>();
                            Log.text = Log.text + "<color=red>Critical damage = " + CritDamageToApply + "</color> \n";
                            Debug.Log("Critical damage " + CritDamageToApply);
                            return 1;
                        }
                        Log.text = Log.text + "<color=red>Critical damage = " + CritDamageToApply + "</color> \n";
                        Debug.Log("Critical damage " + CritDamageToApply);
                        return CritDamageToApply;
                    }
                    int DamageToApply = (int)(((Random.Range(attacker.MinCurrentWeaponAtk, attacker.MaxCurrentWeaponAtk) + MainStat / 2) - (float)defender.PhysicalDef));
                    if (DamageToApply <= 0)
                    {
                        Log.text = Log.text + "<color=green>Hit on = " + DamageToApply + "</color> \n";
                        Debug.Log("Hit on " + DamageToApply);
                        return 1;
                    }
                    Log.text = Log.text + "<color=green>Hit = " + DamageToApply + "</color> \n";
                    Debug.Log("Hit " + DamageToApply);
                    return DamageToApply;
                }
                Log.text = Log.text + "<color=blue>Target evade damage </color> \n";
                Debug.Log("Target evade damage");
            }
            Log.text = Log.text + "<color=blue>Hit missed! </color> \n";
            Debug.Log("Hit missed!");
        }
        return 0;
    }

    public static bool CheckHit(Unit attacker, Unit defender)
    {
        float AttackerHeightAdvantage;

        AttackerHeightAdvantage = checkHeightAdvantage(attacker, defender);

        if (AttackerHeightAdvantage >= 0.5f)
        {
            if (Random.value <= attacker.ToHitChance+0.1f)
            {
                return true;
            }
        }
        else
        {
            if (Random.value <= attacker.ToHitChance)
            {
                return true;
            }
        }
        return false;
    }

    public static bool CheckEvade(Unit attacker, Unit defender)
    {
        if (Random.value <= attacker.EvadeChance)
        {
            return true;
        }
        return false;
    }

    public static bool CheckCrit(Unit attacker, Unit defender)
    {
        if (Random.value <= attacker.CritChance)
        {
            return true;
        }
        return false;
    }

    public static void applyDamage(Unit target, int damage) 
    {
        target.HP.Value -= damage;
        if (target.HP.Value <= 0) 
        {
            MakeDead(target);
        }
    }

    private static void MakeDead(Unit target)
    {
        Animation anim = target.gameObject.GetComponentInChildren<Animation>();
        anim.Play("Death");
        target.State = EnumSpace.unitStates.dead;
        target.OwnerPlayer.SpawnedPartyUnits.Remove(target);
    }

    public static bool checkHeight(Unit attacker, Unit defender) 
    {
        if (Mathf.Abs(attacker.CurrentTile.height - defender.CurrentTile.height) > 1f)
        {
            return false;
        }
        else 
        {
            return true;
        }
    
    }

    public static float checkHeightAdvantage(Unit attacker, Unit defender)
    {
        return attacker.CurrentTile.height - defender.CurrentTile.height;
    }

    public static void ChangeAttribute(BaseAttribute attr,ModType type,int val)
    {
        switch (type)
        {
            case ModType.Add:
                attr.Value += val;
                break;
            case ModType.Sub:
                attr.Value -= val;
                break;
            case ModType.Change:
                attr.Value = val;
                break;
            case ModType.Percent:
                attr.Value = attr.Value * val;
                break;
        }
    }
}
