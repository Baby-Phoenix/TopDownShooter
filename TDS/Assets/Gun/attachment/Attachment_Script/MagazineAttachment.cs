using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Magazine", menuName = "Gun/Attachment/Magazine")]
public class MagazineAttachment : ScriptableObject
{
    //stats control by Magazine
    public GameObject bulletPrefab;

    public bool isRaycast;
    public bool canPenetrate;

    public int baseMagazineSize;
    public float knockbackStrengthModifer;// This should be a percentage betwen 0-1
    public float damageModifer;// This should be a percentage betwen 0-1

    public float baseBulletSpeed;// For Prefab Bullet 

    public float baseStun;

    public int XPToLevelUp;
    private int XP=0;
    int level = 1;
    public virtual float DamageModifier(float baseDamage)
    {
        return 1+ damageModifer;
    }

    public virtual float KnockbackStrengthModifier(float baseKnockbackStrengthModifer)
    {
        return 1 + knockbackStrengthModifer ;
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
