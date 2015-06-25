using UnityEngine;

public static class GameMath {
    //TODO: add case for attack type to choose defence type
    public static int CalculateDamage(Unit attacker, Unit defender)
    {
        int MainStat = 0;

        switch(attacker.UnitClass)
        {
            case EnumSpace.unitClass.knigth:
                MainStat = attacker.Strength;
                break;
            case EnumSpace.unitClass.archer:
                MainStat = attacker.Dexterity;
                break;
            case EnumSpace.unitClass.mage:
                MainStat = attacker.Magic;
                break;
            case EnumSpace.unitClass.squire:
                MainStat = attacker.Dexterity;
                break;
            case EnumSpace.unitClass.none:
                MainStat = attacker.Strength;
                break;
        }


        if (checkHeight(attacker, defender))
        {
            if (CheckHit(attacker, defender))
            {
                if (!CheckEvade(attacker, defender))
                {
                    if (CheckCrit(attacker, defender))
                    {
                        int CritDamageToApply = (int)(((Random.Range(attacker.MinCurrentWeaponAtk, attacker.MaxCurrentWeaponAtk) + MainStat / 2) - (float)defender.PhysicalDef) * attacker.CritMultiplier);
                        if (CritDamageToApply <= 0)
                        {
                            return 1;
                        }
                        return CritDamageToApply;
                    }
                    int DamageToApply = (int)(((Random.Range(attacker.MinCurrentWeaponAtk, attacker.MaxCurrentWeaponAtk) + MainStat / 2) - (float)defender.PhysicalDef));
                    if (DamageToApply <= 0)
                    {
                        return 1;
                    }
                    return DamageToApply;
                }
            }
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
        target.HP -= damage;
    }

    public static bool checkHeight(Unit attacker, Unit defender) 
    {
        if (Mathf.Abs(attacker.currentTile.height - defender.currentTile.height) > 1f)
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
        return attacker.currentTile.height - defender.currentTile.height;
    }
}
