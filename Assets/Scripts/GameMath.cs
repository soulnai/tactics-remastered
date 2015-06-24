using UnityEngine;

public static class GameMath {
    public static int CalculateDamage(Unit attacker, Unit defender)
    {
        if (CheckHit(attacker, defender))
        {
            if (!CheckEvade(attacker, defender))
            {
                if (CheckCrit(attacker, defender))
                {
                    int CritDamageToApply = (int)(((Random.Range(attacker.MinCurrentWeaponAtk, attacker.MaxCurrentWeaponAtk) + attacker.Strength / 2) - (float)defender.PhysicalDef) * attacker.CritMultiplier);
                    if (CritDamageToApply <= 0)
                    {
                        return 1;
                    }
                    return CritDamageToApply;
                }
                int DamageToApply = (int)(((Random.Range(attacker.MinCurrentWeaponAtk, attacker.MaxCurrentWeaponAtk) + attacker.Strength / 2) - (float)defender.PhysicalDef));
                if (DamageToApply <= 0)
                {
                    return 1;
                }
                return DamageToApply;
            }
        }
        return 0;
    }

    public static bool CheckHit(Unit attacker, Unit defender)
    {
        if (Random.value <= attacker.ToHitChance)
        {
            return true;
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
}
