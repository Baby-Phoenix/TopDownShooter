using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarHeadBullet : PrefabBullet
{
    public GameObject muzzleFlash;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="Wall"|| collision.gameObject.tag == "Enemy")
        {
            GameObject effect = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
            Destroy(effect, 5);
            Destroy(gameObject);
        }
    }
}