using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject CharacterUI;

    public UI_ItemDescription itemDescription;

    // Start is called before the first frame update
    void Start()
    {
        //itemDescription = GetComponentInChildren<UI_ItemDescription>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTo(GameObject _menu)  //�л�UI����Ĳ�ͬ�˵�: ������ܡ�������ѡ��
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);  //����ÿһ��������µ������
        }

        if (_menu != null)
        { 
            _menu.SetActive(true);  //��������ض��˵��󽫼���ò˵�
        }
    }

}
