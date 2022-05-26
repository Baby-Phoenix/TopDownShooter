using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public float spread_M;
    public float range_M;

    public virtual float spreadModifier(float baseSpead) { return 0; }
    public virtual float rangeModifier(float baseRange) { return 0; }
}
