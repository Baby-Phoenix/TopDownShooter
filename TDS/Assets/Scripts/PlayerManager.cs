using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private float rayCastDistance = 100;

    GameObject topCamObj;
    GameObject sideCamObj;

    //TwinStickMovement playerMovement;

    private void Start()
    {
        topCamObj = GameObject.FindGameObjectWithTag("TopCam");
        sideCamObj = GameObject.FindGameObjectWithTag("SideCam");

        //playerMovement = new TwinStickMovement();
    }

    private void Update()
    {
        UpdateRoomView();
    }

    private void UpdateRoomView()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.down), rayCastDistance);

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.tag.Equals("Top"))
            {
                topCamObj.SetActive(true);
                sideCamObj.SetActive(false);

                TwinStickMovement.isTopDown = true;
            }
            else if (hit.transform.tag.Equals("Side"))
            {
                topCamObj.SetActive(false);
                sideCamObj.SetActive(true);

                TwinStickMovement.isTopDown = false;
            }
        }
    }
}
