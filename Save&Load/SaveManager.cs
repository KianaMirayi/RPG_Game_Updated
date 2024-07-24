using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditorInternal;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;


    private GameData gameData;

    private List<ISaveManager> saveManagers; //�б�ӵ������ISaveManager�ӿ�
    private FileDataHandler dataHandler;

    [SerializeField] private string fileName;
    [SerializeField] private bool encryptData;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        { 
            instance = this;
        }
    }

    private void Start() //����Ϸ��ʼʱ������Ϸ
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath,fileName,encryptData);
        //dataHandler = new FileDataHandler("D:Game/games", fileName);
        //����ΪApplication.persistentDataPathʱ��C:\Users\Admin\AppData\LocalLow\DefaultCompany\RPG Game�����ҵ��洢������
        //����Ϊ"D:Game/games"ʱ������ָ���ļ����ҵ��洢������

        saveManagers = FindAllSaveManagers(); // �����ص��б��б���saveManagers��

        LoadGame();
    }

    [ContextMenu("Delete saved file")]
    private void DeleteAndSaveData()  //�ڸýű��Ҽ��˵����ɾ���ѱ������Ϸ���ݹ���
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        dataHandler.Delete();
    }
    


    public void NewGame()
    { 
        gameData = new GameData(); //����ʵ����һ��GameData��
    }


    public void LoadGame()
    {
        // gameData = data from dataHandler
        gameData = dataHandler.Load();




        if (this.gameData == null)
        {
            Debug.Log("δ�ҵ��洢����Ϸ����");
            NewGame();
        }


        foreach (ISaveManager saveManager in saveManagers) //��ÿһ��ʵ����ISaveManager�ӿڵĶ��󶼽��м�����Ϸ����
        {
            saveManager.LoadData(gameData);
        }

        Debug.Log("�����ϾֵĽ������" + gameData.currency);

    }


    public void SaveGame()
    {
        //data handler save gameData
        foreach (ISaveManager saveManager in saveManagers) //��ÿһ��ʵ����ISaveManager�ӿڵĶ��󶼽��б�����Ϸ����
        {
            saveManager.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
        Debug.Log("��Ϸ�ѱ���");
        Debug.Log("����Ľ������ " + gameData.currency);
    }

    private void OnApplicationQuit() // Ӧ���˳�ǰ���͸�������Ϸ����
    {
        SaveGame();
        
    }

    private List<ISaveManager> FindAllSaveManagers()  //���ڲ�������ʵ�� ISaveManager �ӿڵ� MonoBehaviour ʵ�����������Ƿ���Ϊһ�� List<ISaveManager>
    { //ʹ���� FindObjectsOfType<MonoBehaviour>() �� OfType<ISaveManager>()��Ȼ�󽫽��ת��Ϊ List<ISaveManager>��
        //ʹ�� LINQ ��չ���� OfType<T>��ɸѡ��ʵ���� ISaveManager �ӿڵĶ���OfType<T> ֻ������Щ����ת��Ϊָ������ T ��Ԫ�ء�


        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }

}
