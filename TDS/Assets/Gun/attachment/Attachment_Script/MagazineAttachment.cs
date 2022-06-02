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

    public virtual float DamageModifier(float baseDamage)
    {
        return 1+ damageModifer;
    }

    public virtual float KnockbackStrengthModifier(float baseKnockbackStrengthModifer)
    {
        return 1 + knockbackStrengthModifer ;
    }
}
