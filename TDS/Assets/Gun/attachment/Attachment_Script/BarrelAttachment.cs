using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Barrel", menuName = "Gun/Attachment/Barrel")]
public class BarrelAttachment : ScriptableObject
{
    public float baseDamage;
    public float baseFireRate;
    public float baseSpread;
    public float baseRange;
    public float baseReloadTime;
    public float baseTimeBetweenShoots;
    public float baseKnockbackStrength;
    public float bulletSpeedModifer;
    public int   baseBulletsPerTap;
    public bool  isShotgun;

    public int XPToLevelUp;
    private int XP=0;
    int level = 1;
    public virtual float BulletSpeedModifer(float bulletSpeed)
    {
        return bulletSpeedModifer + 1;
    }

    public void AddXp(int xp)
    {
        XP = XP + xp;
    }
    public void setXpZero()
    {
        if (level != 10)
        {
            level = level + 1;
            XP = 0;
        }
        else
        {
            XP = 0;
        }
    }
    public void resetLevelToOne()
    {
        level = 1;
        XP = 0;
    }
    public bool isLevelUp()
    {
        if (XP >= XPToLevelUp)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
