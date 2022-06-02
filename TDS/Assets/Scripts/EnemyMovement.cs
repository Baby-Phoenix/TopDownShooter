using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;


[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMovement : MonoBehaviour
{
    public TwinStickMovement currentTarget;
    public float updateSpeed = 0.1f; //How frequently to recalculate path based on target transform's position

    public LayerMask detectionLayer;
    public float detectionRadius = 10;
    public float detectionAngle = 20;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(FollowTarget());
    }

    private void FixedUpdate()
    {
        if (currentTarget == null)
        {
            HandleDetection();
        }
    }

    public IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);

        while (enabled)
        {
            if(currentTarget != null && agent.enabled==true)
                agent.SetDestination(currentTarget.transform.position);

            yield return wait;
        }
    }    

    public void HandleDetection()
    {
        //Find colliders overlapping the sphere
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            //Check to see if player is inside the sphere
            TwinStickMovement twinStickMovement = colliders[i].transform.GetComponent<TwinStickMovement>();

            if (twinStickMovement != null)
            {
                Vector3 targetDirection = twinStickMovement.transform.position - transform.position;

                //Find angle that the player is in and check if it is in FOV
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if (viewableAngle > -detectionAngle / 2 && viewableAngle < detectionAngle / 2)
                {
                    currentTarget = twinStickMovement;
                }
            }
        }

    }

    #region GUI
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        DrawWireArc(transform.position, transform.forward, detectionAngle, detectionRadius);
    }

    public static void DrawWireArc(Vector3 position, Vector3 dir, float anglesRange, float radius, float maxSteps = 20)
    {
        var srcAngles = GetAnglesFromDir(position, dir);
        var initialPos = position;
        var posA = initialPos;
        var stepAngles = anglesRange / maxSteps;
        var angle = srcAngles - anglesRange / 2;
        for (var i = 0; i <= maxSteps; i++)
        {
            var rad = Mathf.Deg2Rad * angle;
            var posB = initialPos;
            posB += new Vector3(radius * Mathf.Cos(rad), 0, radius * Mathf.Sin(rad));

            Gizmos.DrawLine(posA, posB);

            angle += stepAngles;
            posA = posB;
        }
        Gizmos.DrawLine(posA, initialPos);
    }

    static float GetAnglesFromDir(Vector3 position, Vector3 dir)
    {
        var forwardLimitPos = position + dir;
        var srcAngles = Mathf.Rad2Deg * Mathf.Atan2(forwardLimitPos.z - position.z, forwardLimitPos.x - position.x);

        return srcAngles;
    }
    #endregion
}

