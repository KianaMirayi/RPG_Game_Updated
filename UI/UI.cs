using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class UI : MonoBehaviour,ISaveManager
{
    [SerializeField] private GameObject CharacterUI;
    [SerializeField] private GameObject SkillTreeUI;
    [SerializeField] private GameObject CraftUI;
    [SerializeField] private GameObject OptionsUI;
    [SerializeField] private GameObject InGameUI;
    [Space]
    [Header("End Screen")]
    [SerializeField] public UI_FadeScreen FadeScreen;
    [SerializeField] private GameObject EndText;
    [SerializeField] private GameObject RestartButton;

    [Space]

    public UI_ItemDescription itemDescription;
    public UI_StatDescription statDescription;
    public UI_CraftWindow craftWindow;
    public UI_SkillDescription skillDescription;

    [SerializeField] private UI_VolumeSlider[] volumeSettings;
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
            bool FadeScreen =transform.GetChild(i).GetComponent<UI_FadeScreen>() != null;  //���������������ʱ�������


            if (FadeScreen == false)
            {
                transform.GetChild(i).gameObject.SetActive(false);  //����ÿһ��������µ������
            }

            
        }

        if (_menu != null)
        {
            AudioManager.instance.PlaySfx(7, null);

            _menu.SetActive(true);  //��������ض��˵��󽫼���ò˵�
        }

        if (GameManager.instance != null)
        {
            if (_menu == InGameUI) //�������Ϸ��
            {
                GameManager.instance.PauseGame(false);
            }
            else
            {
                GameManager.instance.PauseGame(true); //��������κ����
            }
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
            if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<UI_FadeScreen>() == null)
            {
                return;
            }
        }

        SwitchTo(InGameUI);
    }

    public void SwitchOnEndScreen() //��ɫ����ʱ����
    { 
        
        FadeScreen.FadeOut();
        StartCoroutine(EndScreenCoroutine());


    }

    IEnumerator EndScreenCoroutine()
    { 
        yield return new WaitForSeconds(1);
        EndText.SetActive(true);
        yield return new WaitForSeconds(2); // һ�����ʾ����
        RestartButton.SetActive(true);

    }

    public void RestartGameButton() //���¿�ʼ��Ϸ
    { 
        GameManager.instance.RestartScene();
    }

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, float> pair in _data.volumeSettings)
        {
            foreach (UI_VolumeSlider item in volumeSettings)
            {
                if (item.paramater == pair.Key)
                {
                    item.LoadSlider(pair.Value);
                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.volumeSettings.Clear();

        foreach (UI_VolumeSlider item in volumeSettings)
        {
            _data.volumeSettings.Add(item.paramater, item.slider.value);
        }
    }
}
