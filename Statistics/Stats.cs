using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stats
{
    [SerializeField] private int BaseValue;

    public List<int> Modifiers;

    public int GetValue()
    {
        int finalValue = BaseValue;

        foreach (int value in Modifiers)
        { 
            finalValue += value;
        }

        return finalValue;
    }

    public void SetDefaultValue(int _value)
    { 
        BaseValue = _value;
    }

    public void AddModifier(int _modifier)
    { 
        Modifiers.Add(_modifier);
    }

    public void RemoveModifier(int _modifier)
    { 
        Modifiers.Remove(_modifier);
    }
}
