using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{

    [Header("References")]
    public Transform orientation;
    public CharacterController character; //try and use charcter controller
    public LayerMask whatIsWall;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;

    private bool climbing;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;


    private void Update()
    {
        
    }

    private void StateMachine()
    {
        //state 1 - climbing
        if(wallFront && )

    }


    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward,
                                       out frontWallHit, detectionLength, whatIsWall);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);
    }

    private void StartClimbing()
    {
        climbing = true;
    }
    private void ClimbingMovement()
    {
        //***** this could be a problem depending on how character controller works with rigid body
        character.attachedRigidbody.velocity = new Vector3(character.velocity.x, climbSpeed, character.velocity.z);
        
    }


    private void StopClimbing()
    {
        climbing = false;
    }
}
