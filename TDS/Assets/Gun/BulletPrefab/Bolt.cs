using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bolt : PrefabBullet
{
    public GameObject muzzleFlash;
    public float KnockbackStrength;
    void Awake()
    {
        Physics.IgnoreLayerCollision(6, 7);
    }
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Wall")
        {
            gameObject.GetComponent<Rigidbody>().velocity= new Vector3 (0,0,0);
            GameObject effect = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
            Destroy(effect, 5);
            Destroy(gameObject,0.5f);
        }
        else
        {
            Destroy(gameObject, 2f);
        }
    }
}