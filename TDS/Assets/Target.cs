using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    Vector3 v;
    bool IsV;
    public float health = 50f;
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
     
        if (gameObject.GetComponent<NavMeshAgent>().enabled == false&& NearZeroVector(gameObject.GetComponent<Rigidbody>().velocity))
        {
            Debug.Log("Hi");
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            gameObject.GetComponent<EnemyMovement>().FollowTarget();
        }
    }
    public bool NearZeroVector(Vector3 v)
    {
        return v.sqrMagnitude < 0.1;
    }
}
