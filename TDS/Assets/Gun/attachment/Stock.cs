using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    //stats control by Stock
    public float spread_M;

    public virtual float spreadModifier(float baseSpead) { return 0; }
}
