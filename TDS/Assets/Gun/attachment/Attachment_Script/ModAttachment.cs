using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mod", menuName = "Gun/Attachment/Mod")]

public class ModAttachment : ScriptableObject
{
    public ModFunction f1;
    public ModFunction f2;
    public ModFunction f3;

    private ModFunction.EffectType effectType1;
    private ModFunction.Effect effect1;

    private ModFunction.EffectType effectType2;
    private ModFunction.Effect effect2;

    private ModFunction.EffectType effectType3;
    private ModFunction.Effect effect3;

    private float damageModifier;
    private float fireRateModifier;
    private float spreadModifier;
    private float rangeModifier;
    private float reloadTimeModifier;
    private float timeBetweenShootsModifier;
    private int bulletPerTapModifier;
    private float bulletSpeedModifier;
    private int magazineSizeModifier;
    private float knockbackStrengthModifier;

    public void setModAttachmentModifier()
    {
        damageModifier = (1+f1.damageModifier) * (1 + f2.damageModifier) * (1 + f3.damageModifier);
        fireRateModifier = (1 + f1.fireRateModifier) * (1 + f2.fireRateModifier) * (1 + f3.fireRateModifier);
        spreadModifier = (1- f1.spreadModifier) * (1 - f2.spreadModifier) * (1 - f3.spreadModifier);
        rangeModifier = (1+f1.rangeModifier) *  (1 + f2.rangeModifier) * (1 + f3.rangeModifier);
        reloadTimeModifier = (1 - f1.reloadTimeModifier) * (1 - f2.reloadTimeModifier) * (1 - f3.reloadTimeModifier);
        timeBetweenShootsModifier = (1-f1.timeBetweenShootsModifier) * (1-f2.timeBetweenShootsModifier) + (1-f3.timeBetweenShootsModifier);
        bulletPerTapModifier = (1+f1.bulletPerTapModifier) * (1+f2.bulletPerTapModifier) * (1+f3.bulletPerTapModifier);
        bulletSpeedModifier = (1+f1.bulletSpeedModifier) * (1+f2.bulletSpeedModifier) * (1+f3.bulletSpeedModifier);
        magazineSizeModifier = (1+f1.magazineSizeModifier) * (1+f2.magazineSizeModifier) * (1+f3.magazineSizeModifier);
        knockbackStrengthModifier = (1 + f1.knockbackStrengthModifier) * (1 + f2.knockbackStrengthModifier) * (1 + f3.knockbackStrengthModifier);

        effectType1 = f1.effectType;
        effectType2 = f2.effectType;
        effectType3 = f3.effectType;

        effect1 = f1.effect;
        effect2 = f2.effect;
        effect3 = f3.effect;
    }

    public virtual float DamageModifier(float baseDamage)
    {
        return damageModifier;
    }
    public virtual float FireRateModifier(float baseFireRate)
    {
        return fireRateModifier;
    }
    public virtual float SpreadModifier(float baseSpread)
    {
        return spreadModifier;
    }
    public virtual float RangeModifier(float baseRange)
    {
        return rangeModifier;
    }
    public virtual float ReloadTimeModifier(float baseReloadTime)
    {
        return reloadTimeModifier;
    }
    public virtual int BulletPerTapModifier(int baseBulletPerTap)
    {
        return bulletPerTapModifier;
    }
    public virtual float BulletSpeedModifier(float baseBulletSpeed)
    {
        return bulletSpeedModifier;
    }
    public virtual int MagazineSizeModifier(int baseMagazineSize)
    {
        return magazineSizeModifier;
    }
    public virtual float KnockbackStrengthModifier(float baseKnockbackStrength)
    {
        return knockbackStrengthModifier;
    }

    public ModFunction.EffectType GetEffectType1()
    {
        return effectType1;
    }
    public ModFunction.EffectType GetEffectType2()
    {
        return effectType2;
    }
    public ModFunction.EffectType GetEffectType3()
    {
        return effectType3;
    }

    public ModFunction.Effect GetEffect1()
    {
        return effect1;
    }
    public ModFunction.Effect GetEffect2()
    {
        return effect2;
    }
    public ModFunction.Effect GetEffect3()
    {
        return effect3;
    }
}
