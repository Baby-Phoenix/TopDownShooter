using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    //stats control by Magazine
    public bool IsRayCast; 
    public int magazineSize_B;
    public float KnockbackStrength;

    public virtual int getMagazineSize()
    {
        return magazineSize_B;
    }
}
