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

    public virtual float BulletSpeedModifer(float bulletSpeed)
    {
        return bulletSpeed * bulletSpeedModifer;
    }
}
