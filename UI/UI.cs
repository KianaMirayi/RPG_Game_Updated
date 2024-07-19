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

    public void SwitchTo(GameObject _menu)  //切换UI界面的不同菜单: 人物、技能、工坊、选项
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);  //遍历每一个该组件下的自组件
        }

        if (_menu != null)
        { 
            _menu.SetActive(true);  //当鼠标点击特定菜单后将激活该菜单
        }
    }

}
