using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scope", menuName = "Gun/Attachment/Scope")]
public class ScopeAttachment : ScriptableObject
{
    public float spreadModifer;// This should be a percentage betwen 0-1
    public float rangeModifer;// This should be a percentage betwen 0-1

    public int XPToLevelUp;
    private int XP=0;
    int level = 1;
    public virtual float SpreadModifier(float baseSpead) {
        return 1 - spreadModifer;
    }
    public virtual float RangeModifier(float baseRange) {
        return 1 + rangeModifer;
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
