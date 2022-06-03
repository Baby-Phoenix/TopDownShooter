using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ModFunction", menuName = "Gun/Mod")]
public class ModEffect : ScriptableObject
{
    public enum EffectType { NoEffect = 0, EffectOnBulletHitEnemy = 1, EffectOnBulletHit = 2, EffectOnFiring = 3, Enchant = 4 };
    public enum Effect { Explosive = 0 };

    public EffectType type;

    public delegate void ModFunction();

    public GameObject explosion;
    List<ModFunction> mod;
    void Explosive(Vector3 pos)
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
