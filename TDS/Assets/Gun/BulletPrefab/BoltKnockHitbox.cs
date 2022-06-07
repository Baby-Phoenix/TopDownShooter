using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoltKnockHitbox : BoltBullet
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy"&& other.gameObject.GetComponent<Target>()!=null)
        {
            //mod.ModEffect1(new Vector3(0, 0, 0));
            GameObject effect = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
            other.gameObject.GetComponent<Target>().TakeDamage(transform.parent.GetComponent<BoltBullet>().getDamage());
            other.gameObject.GetComponent<Target>().Stun(transform.parent.GetComponent<BoltBullet>().getStun());
            f1 = transform.parent.GetComponent<BoltBullet>().f1;
            f2 = transform.parent.GetComponent<BoltBullet>().f2;
            f3 = transform.parent.GetComponent<BoltBullet>().f3;
            EffectOnHit();
            Destroy(effect, 5);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy"&& other.gameObject.GetComponent<Rigidbody>() != null)
        {

            //mod.ModEffect1(new Vector3(0, 0, 0));
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            rb.AddForce(transform.parent.GetComponent<BoltBullet>().getKnockbackDirection().normalized * transform.parent.GetComponent<BoltBullet>().getKnockbackStrength(), ForceMode.Impulse);
            if (other.gameObject.GetComponent<NavMeshAgent>().isActiveAndEnabled && other.gameObject.GetComponent<Target>().IsStun())
            {
                other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
    }
}
