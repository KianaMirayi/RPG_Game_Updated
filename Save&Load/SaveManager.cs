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

    private List<ISaveManager> saveManagers; //列表，拥有所有ISaveManager接口
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

    private void Start() //在游戏开始时加载游戏
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath,fileName,encryptData);
        //dataHandler = new FileDataHandler("D:Game/games", fileName);
        //设置为Application.persistentDataPath时在C:\Users\Admin\AppData\LocalLow\DefaultCompany\RPG Game可以找到存储的数据
        //设置为"D:Game/games"时可以在指定文件夹找到存储的数据

        saveManagers = FindAllSaveManagers(); // 将返回的列表列表传进saveManagers中

        LoadGame();
    }

    [ContextMenu("Delete saved file")]
    private void DeleteAndSaveData()  //在该脚本右键菜单添加删除已保存的游戏数据功能
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        dataHandler.Delete();
    }
    


    public void NewGame()
    { 
        gameData = new GameData(); //重新实例化一个GameData类
    }


    public void LoadGame()
    {
        // gameData = data from dataHandler
        gameData = dataHandler.Load();




        if (this.gameData == null)
        {
            Debug.Log("未找到存储的游戏数据");
            NewGame();
        }


        foreach (ISaveManager saveManager in saveManagers) //对每一个实现了ISaveManager接口的对象都进行加载游戏数据
        {
            saveManager.LoadData(gameData);
        }

        Debug.Log("加载上局的金币数：" + gameData.currency);

    }


    public void SaveGame()
    {
        //data handler save gameData
        foreach (ISaveManager saveManager in saveManagers) //对每一个实现了ISaveManager接口的对象都进行保存游戏数据
        {
            saveManager.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
        Debug.Log("游戏已保存");
        Debug.Log("保存的金币数： " + gameData.currency);
    }

    private void OnApplicationQuit() // 应用退出前发送给所有游戏对象
    {
        SaveGame();
        
    }

    private List<ISaveManager> FindAllSaveManagers()  //用于查找所有实现 ISaveManager 接口的 MonoBehaviour 实例，并将它们返回为一个 List<ISaveManager>
    { //使用了 FindObjectsOfType<MonoBehaviour>() 和 OfType<ISaveManager>()，然后将结果转换为 List<ISaveManager>。
        //使用 LINQ 扩展方法 OfType<T>，筛选出实现了 ISaveManager 接口的对象。OfType<T> 只返回那些可以转换为指定类型 T 的元素。


        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }

}
