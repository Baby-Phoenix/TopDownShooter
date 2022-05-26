using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scope", menuName = "Gun/Attachment/Scope")]
public class ScopeAttachment : ScriptableObject
{
    public float spreadModifer;// This should be a percentage betwen 0-1
    public float rangeModifer;// This should be a percentage betwen 0-1

    public virtual float SpreadModifier(float baseSpead) {
        return baseSpead * spreadModifer;
    }
    public virtual float RangeModifier(float baseRange) {
        return baseRange * rangeModifer;
    }
}
