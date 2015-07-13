using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using EnumSpace;

public static class GameMath {
    //TODO: add case for attack type to choose defence type

    public static bool TryHit(Unit attacker, Unit defender)
    {
        bool hit = false;

        if (checkHeight(attacker, defender))
        {
            if (CheckHit(attacker, defender))
            {
                if (!CheckEvade(attacker, defender))
                {
                    hit = true;
                }
                Debug.Log("Target evade damage");
            }
            Debug.Log("Hit missed!");
        }

        return hit;
    }
    public static int CalculateDamage(Unit attacker, Unit defender)
    {
        if (CheckCrit(attacker, defender))
        {
            int CritDamageToApply =
                (int)
                    (((Random.Range(attacker.MinCurrentWeaponAtk, attacker.MaxCurrentWeaponAtk) + MainStat(attacker)/2) -
                      (float) defender.PhysicalDef)*attacker.CritMultiplier);
            if (CritDamageToApply <= 0)
            {
                Debug.Log("Critical damage " + CritDamageToApply);
                return 1;
            }
            Debug.Log("Critical damage " + CritDamageToApply);
            return CritDamageToApply;
        }

        int DamageToApply = (int)(((Random.Range(attacker.MinCurrentWeaponAtk, attacker.MaxCurrentWeaponAtk) + MainStat(attacker) / 2) - (float)defender.PhysicalDef));
        
        if (DamageToApply <= 0)
        {
            Debug.Log("Hit on " + DamageToApply);
            return 1;
        }
        Debug.Log("Hit " + DamageToApply);
        return DamageToApply;
    }

    private static int MainStat(Unit attacker)
    {
        int MainStat = 0;

        switch (attacker.UnitClass)
        {
            case unitClass.knigth:
                MainStat = attacker.Strenght.Value;
                break;
            case unitClass.warrior:
                MainStat = attacker.Strenght.Value;
                break;
            case unitClass.archer:
                MainStat = attacker.Dexterity.Value;
                break;
            case unitClass.mage:
                MainStat = attacker.Magic.Value;
                break;
            case unitClass.squire:
                MainStat = attacker.Dexterity.Value;
                break;
            case unitClass.none:
                MainStat = attacker.Strenght.Value;
                break;
        }
        return MainStat;
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
