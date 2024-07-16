using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item Data", menuName ="Data/Item Effect")]
public class ItemEffect : ScriptableObject
{
    public virtual void ExcuteEffect()
    {
        Debug.Log("Effect executed");
    }
}
