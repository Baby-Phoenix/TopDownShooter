using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed;
    private GameObject player;
    private void Update()
    {
        transform.position = player.transform.position;
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }
    public void setPlayer(GameObject p)
    {
        player = p;
    }
}
