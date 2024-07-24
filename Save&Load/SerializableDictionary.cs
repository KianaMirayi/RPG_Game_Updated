using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;

[System.Serializable]
//��ÿһ��unity���ʲ���Id���洢���ļ���
public class SerializableDictionary<Tkey,TValue> : Dictionary<Tkey,TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<Tkey> keys = new List<Tkey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    public void OnBeforeSerialize() 
    {
        //throw new System.NotImplementedException();

        keys.Clear();
        values.Clear();

        foreach (KeyValuePair<Tkey, TValue> pair in this)
        { 
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }


    public void OnAfterDeserialize()
    {
        //throw new System.NotImplementedException();

        this.Clear();

        if (keys.Count != values.Count)
        {
            Debug.Log("����ֵ�б��Ȳ���");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }
    }

   

    
}
