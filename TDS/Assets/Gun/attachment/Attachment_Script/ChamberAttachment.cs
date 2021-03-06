using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chamber", menuName = "Gun/Attachment/Chamber")]
public class ChamberAttachment : ScriptableObject
{
    public bool allowButtonHold;
    public bool isHandLoaded;

    public float damageModifer;
    public float fireRateModifer;// This should be a percentage betwen 0-1
    public float reloadTimeModifer;

    public int XPToLevelUp;
    private int XP=0;
    int level = 1;
    public virtual float DamageModifier(float baseDamage)
    {
        return damageModifer +1;
    }

    public virtual float FireRateModifier(float baseFiringSpeed)
    {
        return 1-fireRateModifer;
    }

    public virtual float ReloadTimeModifier(float baseReloadTime)
    {
        return 1- reloadTimeModifer;
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
