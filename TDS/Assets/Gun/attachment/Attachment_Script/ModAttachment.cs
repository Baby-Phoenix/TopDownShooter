using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mod", menuName = "Gun/Attachment/Mod")]

public class ModAttachment : ScriptableObject
{

    //For modAttachment
    public delegate void OnBulletHitEnemyModEffect(Vector3 pos);
    public static event OnBulletHitEnemyModEffect OnBulletHitEnemy;

    public delegate void OnBulletHitModEffect(Vector3 pos);
    public static event OnBulletHitModEffect OnBulletHit;

    public delegate void OnFiringModEffect(Vector3 pos);
    public static event OnFiringModEffect OnFiring;
    public enum EffectType { NoEffect = 0, EffectOnBulletHitEnemy = 1, EffectOnBulletHit = 2, EffectOnFiring = 3, Enchant = 4 };
    public enum Effect { Explosive = 0 };

    public EffectType effectType1;
    public EffectType effectType2;
    public EffectType effectType3;

    public Effect effect1;
    public Effect effect2;
    public Effect effect3;

    public static GameObject explosion;
    private Vector3 position;


    public void OnEnable()
    {
        //Firearm.OnBulletHitEnemy += ModEffect1;
    }
    public void OnDisable()
    {

        //Firearm.OnBulletHitEnemy -= ModEffect1;
    }

    public virtual void ModEffect1(){
        if (effectType1 != EffectType.NoEffect)
        {
            if (effectType1 == EffectType.EffectOnBulletHitEnemy)
            {
                switch (effect1) {
                    case Effect.Explosive:
                        OnBulletHitEnemy += Explosive;
                        break;
                }
            }
            else if (effectType1 == EffectType.EffectOnBulletHit)
            {

            }else if(effectType1 == EffectType.EffectOnFiring)
            {

            }
            else
            {

            }
        }
    }
    public virtual void ModEffect2() { 
    
    }
    public virtual void ModEffect3() {
    
    }


    public static void Explosive(Vector3 pos)
    {
        Instantiate(explosion, pos, Quaternion.identity);
        Collider[] enemies = Physics.OverlapSphere(pos, 5);
        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log(enemies[i]);
            if (enemies[i].GetComponent<Rigidbody>())
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(100, pos, 5);
            }
        }
    }

}
