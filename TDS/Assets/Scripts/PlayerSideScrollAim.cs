using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSideScrollAim : MonoBehaviour
{
    [SerializeField] Transform firePos;
    [SerializeField] Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20;
    [SerializeField] LayerMask aimMask;
    public Vector2 mousePos;
    public float angle = 0;

    private void Update()
    {
        if (!GetComponent<TwinStickMovement>().TopDown())
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
            {
                aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
                aimPos.position = new Vector3(aimPos.position.x, aimPos.position.y, firePos.position.z);
            }

            HandleAngleFormation();
        }

        //if it is topdown the angle doesnt matter and is set to 0 to look straight 
        else
            GetComponent<TwinStickMovement>().anim.SetFloat("Aim", 0);
        
    }

    void HandleAngleFormation()
    {
        float opposite = 0, adjacent = 0;
        Vector3 point1 = firePos.position; //player position
        Vector3 point2 = aimPos.position; //sphere rotation
        Vector3 point3 = new Vector3(point2.x, point1.y, point2.z); //imaginary third point

        opposite = Mathf.Sqrt((Sq(point2.x - point3.x) + (Sq(point2.y - point3.y)) + (Sq(point2.z - point3.z))));

        if (opposite == 0)
        {
            angle = 0;
        }
        else
        {
            adjacent = Mathf.Sqrt((Sq(point1.x - point3.x) + (Sq(point1.y - point3.y)) + (Sq(point1.z - point3.z))));

            angle =  (point2.y < point1.y)? -(Mathf.Rad2Deg * Mathf.Atan(opposite / adjacent)): (Mathf.Rad2Deg * Mathf.Atan(opposite / adjacent));

            GetComponent<TwinStickMovement>().anim.SetFloat("Aim", angle);
        }
    }

    float Sq(float one)
    {
        return one * one;
    }

}
