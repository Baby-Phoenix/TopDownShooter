using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarHeadBullet : PrefabBullet
{
    public GameObject muzzleFlash;
    public bool explodeOnTouch = true;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="Wall"|| collision.gameObject.tag == "Enemy")
        {
            if(explodeOnTouch == true && isExplosive == true)
            {
                Explode();
            }

            GameObject effect = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
            Destroy(effect, 5);
            Destroy(gameObject);
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }


}