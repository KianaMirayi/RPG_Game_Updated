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
        SwitchTo(SkillTreeUI);  //�ڷ��似�ܸ�����UI֮ǰ�����ü���UI
    }

    // Start is called before the first frame update
    void Start()
    {
        //itemDescription = GetComponentInChildren<UI_ItemDescription>(); 
        SwitchTo(null);//���ÿ�ʼ��Ϸʱû�н�ɫUI����
        SwitchTo(InGameUI);
        itemDescription.gameObject.SetActive(false);
        statDescription.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))  //����L�������ɫ�˵�
        {
            SwitchKeyTo(CharacterUI);
        }

        if (Input.GetKeyDown(KeyCode.K))  //����K�����뼼�ܲ˵�
        {
            SwitchKeyTo(SkillTreeUI);
        }

        if (Input.GetKeyDown(KeyCode.I)) //����I�����빤��̨�˵�
        {
            SwitchKeyTo(CraftUI);
        }

        if (Input.GetKeyDown(KeyCode.O))  //����O������ѡ��˵�
        {
            SwitchKeyTo(OptionsUI);
        }
        
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
