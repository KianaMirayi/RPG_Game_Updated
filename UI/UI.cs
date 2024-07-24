using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject CharacterUI;
    [SerializeField] private GameObject SkillTreeUI;
    [SerializeField] private GameObject CraftUI;
    [SerializeField] private GameObject OptionsUI;
    [SerializeField] private GameObject InGameUI;

    public UI_ItemDescription itemDescription;
    public UI_StatDescription statDescription;
    public UI_CraftWindow craftWindow;
    public UI_SkillDescription skillDescription;

    private void Awake()
    {
        SwitchTo(SkillTreeUI);  //在分配技能给技能UI之前先启用技能UI
    }

    // Start is called before the first frame update
    void Start()
    {
        //itemDescription = GetComponentInChildren<UI_ItemDescription>(); 
        SwitchTo(null);//设置开始游戏时没有角色UI界面
        SwitchTo(InGameUI);
        itemDescription.gameObject.SetActive(false);
        statDescription.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))  //按下L键进入角色菜单
        {
            SwitchKeyTo(CharacterUI);
        }

        if (Input.GetKeyDown(KeyCode.K))  //按下K键进入技能菜单
        {
            SwitchKeyTo(SkillTreeUI);
        }

        if (Input.GetKeyDown(KeyCode.I)) //按下I键进入工作台菜单
        {
            SwitchKeyTo(CraftUI);
        }

        if (Input.GetKeyDown(KeyCode.O))  //按下O键进入选项菜单
        {
            SwitchKeyTo(OptionsUI);
        }
        
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


    public void SwitchKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            ActiveInGameUI();
            return;
        }

        SwitchTo(_menu);
    }


    public void ActiveInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                return;
            }
        }

        SwitchTo(InGameUI);
    }


}
