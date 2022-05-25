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

    public virtual float DamageModifier(float baseDamage)
    {
        return baseDamage * damageModifer;
    }

    public virtual float FireRateModifier(float baseFiringSpeed)
    {
        return baseFiringSpeed * fireRateModifer;
    }
}
