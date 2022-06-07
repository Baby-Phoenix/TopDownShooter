using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    Vector3 v;
    bool IsV;
    public float health = 50f;
    public float stunResistance = 3;
    public float stunPoint = 0;
    public bool stuning=false;
    public int XP;

    //exp system
    public delegate void OnKillingEnemy(int xp);
    public static event OnKillingEnemy AddXP;
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
    public void Stun(float amount)
    {
        stunPoint += amount;
    }
    void Die()
    {
        if(AddXP != null)
        {
            AddXP(XP);
        }
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (gameObject.GetComponent<NavMeshAgent>().enabled == false&& NearZeroVector(gameObject.GetComponent<Rigidbody>().velocity))
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            gameObject.GetComponent<EnemyMovement>().FollowTarget();
        }
    }
    public bool NearZeroVector(Vector3 v)
    {
        return v.sqrMagnitude < 0.1;
    }

    public bool IsStun()
    {
        if (stunPoint >= stunResistance&& stuning==false)
        {
            stunPoint = 0;
            stuning = true;
            return true;
        }else if (stunPoint >= stunResistance && stuning == true)
        {
            stunPoint = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}
