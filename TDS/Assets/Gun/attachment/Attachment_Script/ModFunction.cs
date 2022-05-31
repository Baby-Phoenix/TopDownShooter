using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mod Function", menuName = "Gun/Attachment/ModFunction")]

public class ModFunction : ScriptableObject
{
    public enum EffectType { NoEffect = 0, EffectOnBulletHitEnemy = 1, EffectOnBulletHit = 2, EffectOnFiring = 3, Enchant = 4 };
    public enum Effect { NoEffect = 0, Explosive = 1 , DualMode = 2};


    public EffectType effectType;

    public Effect effect;

    public float damageModifier;
    public float fireRateModifier;
    public float spreadModifier;
    public float rangeModifier;
    public float reloadTimeModifier;
    public float timeBetweenShootsModifier;
    public int bulletPerTapModifier;
    public float bulletSpeedModifier;
    public int magazineSizeModifier;
    public float knockbackStrengthModifier;

}
