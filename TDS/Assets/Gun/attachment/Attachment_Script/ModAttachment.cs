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

    public GameObject explosion;
    private Vector3 position;


    public void OnEnable()
    {
        //Firearm.OnBulletHitEnemy += ModEffect1;
    }
    public void OnDisable()
    {

        //Firearm.OnBulletHitEnemy -= ModEffect1;
    }

    public virtual void ModEffect1(Vector3 pos){
        if (effectType1 != EffectType.NoEffect) // if there is a effect on the mod
        {
            if (effectType1 == EffectType.EffectOnBulletHitEnemy) // if the effect type is to appear when collided with the enemy
            {
                switch (effect1) {
                    case Effect.Explosive:
                        OnBulletHitEnemy += Explosive; //the explosion fuction is added to the OnbulletHitenemy fuction which call does the explosion
                        break;
                }
                OnBulletHitEnemy(pos);
            }
            else if (effectType1 == EffectType.EffectOnBulletHit)// if the effect type is to appear when collided with anything
            {
                switch (effect1)
                {
                    case Effect.Explosive:
                        OnBulletHit += Explosive; //the explosion fuction is added to the OnBulletHit fuction which call does the explosion
                        break;
                }

                OnBulletHit(pos);
            }
            else if(effectType1 == EffectType.EffectOnFiring)// if the effect type is to appear when the bullet is fired
            {
                switch (effect1)
                {
                    case Effect.Explosive:
                        OnFiring += Explosive; //the explosion fuction is added to the OnFiring fuction which call does the explosion
                        break;
                }
                OnFiring(pos);
            }
            else //Enchant
            {

            }
        }
    }
    public virtual void ModEffect2() { 
    
    }
    public virtual void ModEffect3() {
    
    }


    public void Explosive(Vector3 pos)
    {
        Debug.Log(pos);
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
