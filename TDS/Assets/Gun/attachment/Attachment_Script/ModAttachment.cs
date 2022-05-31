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
        damageModifier = f1.damageModifier + f2.damageModifier + f3.damageModifier;
        fireRateModifier = f1.fireRateModifier + f2.fireRateModifier + f3.fireRateModifier;
        spreadModifier = f1.spreadModifier + f2.spreadModifier + f3.spreadModifier;
        rangeModifier = f1.rangeModifier + f2.rangeModifier + f3.rangeModifier;
        reloadTimeModifier = f1.reloadTimeModifier + f2.reloadTimeModifier + f3.reloadTimeModifier;
        timeBetweenShootsModifier = f1.timeBetweenShootsModifier + f2.timeBetweenShootsModifier + f3.timeBetweenShootsModifier;
        bulletPerTapModifier = f1.bulletPerTapModifier + f2.bulletPerTapModifier + f3.bulletPerTapModifier;
        bulletSpeedModifier = f1.bulletSpeedModifier + f2.bulletSpeedModifier + f3.bulletSpeedModifier;
        magazineSizeModifier = f1.magazineSizeModifier + f2.magazineSizeModifier + f3.magazineSizeModifier;
        knockbackStrengthModifier = f1.knockbackStrengthModifier + f2.knockbackStrengthModifier + f3.knockbackStrengthModifier;

        effectType1 = f1.effectType;
        effectType2 = f2.effectType;
        effectType3 = f3.effectType;

        effect1 = f1.effect;
        effect2 = f2.effect;
        effect3 = f3.effect;
    }

    public virtual float DamageModifier(float baseDamage)
    {
        return baseDamage * damageModifier;
    }
    public virtual float FireRateModifier(float baseFireRate)
    {
        return baseFireRate * fireRateModifier;
    }
    public virtual float SpreadModifier(float baseSpread)
    {
        return baseSpread * spreadModifier;
    }
    public virtual float RangeModifier(float baseRange)
    {
        return baseRange * rangeModifier;
    }
    public virtual float ReloadTimeModifier(float baseReloadTime)
    {
        return baseReloadTime * reloadTimeModifier;
    }
    public virtual int BulletPerTapModifier(int baseBulletPerTap)
    {
        return baseBulletPerTap * bulletPerTapModifier;
    }
    public virtual float BulletSpeedModifier(float baseBulletSpeed)
    {
        return baseBulletSpeed * bulletSpeedModifier;
    }
    public virtual int MagazineSizeModifier(int baseMagazineSize)
    {
        return baseMagazineSize * magazineSizeModifier;
    }
    public virtual float KnockbackStrengthModifier(float baseKnockbackStrength)
    {
        return baseKnockbackStrength * knockbackStrengthModifier;
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
