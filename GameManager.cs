using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.SubsystemsImplementation;
public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;

    private Transform player; 

    [SerializeField] private CheckPoint[] checkPoints;
    [SerializeField] private string ClosetCheckPointLoaded;

    [Header("Lost Currency")]
    [SerializeField] private GameObject lostCurrencyPrefab;
    public int lostCurrencyAmount;
    [SerializeField] private float lostCurrencyX;
    [SerializeField] private float lostCurrencyY;

    private void Awake()  //Awake > Load > Start
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }

        //Debug.Log("Awake");
        checkPoints = FindObjectsOfType<CheckPoint>(); //注册所有游戏场景中的检查点
    }

    private void Start()
    {
        player = PlayerManager.instance.player.transform;

        //Debug.Log("Found " + checkPoints.Length + " check points.");

    //    foreach (var checkpoint in checkPoints)
    //{
    //    //Debug.Log("CheckPoint ID: " + checkpoint.id);
    //}


    }


    public void RestartScene()  //重新开始游戏
    {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data)
    {
        StartCoroutine(LoadWithDelay(_data));

        //Debug.Log("加载检查点数据");
        //Debug.Log("CheckPoints in data: " + _data.checkPoints.Count);

        //ClosetCheckPointLoaded = _data.closetCheckPointId;
        //Debug.Log("最近检查点已分配" + ClosetCheckPointLoaded);

        //foreach (KeyValuePair<string, bool> pair in _data.checkPoints)
        //{
        //    Debug.Log("正在遍历数组中的键值对");
        //    Debug.Log("Key: " + pair.Key + ", Value: " + pair.Value);

        //    foreach (CheckPoint checkPoint in checkPoints)
        //    {
        //        Debug.Log("准备对每一个键值对进行匹配");
        //        Debug.Log("CheckPoint ID: " + checkPoint.id + ", Activation Status: " + checkPoint.activationStaus);


        //        if (checkPoint.id == pair.Key)
        //        {
        //            Debug.Log("匹配键");

        //            //checkPoint.activationStaus = pair.Value;

        //            if (pair.Value == true)
        //            {
        //                Debug.Log("匹配值");
        //                checkPoint.ActiveCheckPoint();
        //            }

        //            Debug.Log("上局检查点数据已加载");
        //        }
        //    }
        //}



        //foreach (CheckPoint checkPoint in checkPoints)
        //{
        //    if (_data.closetCheckPointId == checkPoint.id)
        //    { 
        //        PlayerManager.instance.player.transform.position = checkPoint.transform.position;

        //        Debug.Log("11111111");
        //    }
        //}

        //Debug.Log("GameManager Loaded");


    }

    

    private void LoadCheckpoints(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.checkPoints)
        {
            foreach (CheckPoint checkpoint in checkPoints)
            {
                if (checkpoint.id == pair.Key && pair.Value == true)
                    checkpoint.ActiveCheckPoint();
            }
        }
    }

    private void LoadClosestCheckpoint(GameData _data)
    {
        if (_data.closetCheckPointId == null)
            return;


        ClosetCheckPointLoaded = _data.closetCheckPointId;

        foreach (CheckPoint checkpoint in checkPoints)
        {
            if (ClosetCheckPointLoaded == checkpoint.id)
            {
                PlayerManager.instance.player.transform.position = checkpoint.transform.position;
            }

        }
    }

    private void LoadLostCurrency(GameData _data)
    {
        lostCurrencyAmount = _data.lostCurrencyAmount;
        lostCurrencyX = _data.lostCurrencyX;
        lostCurrencyY = _data.lostCurrencyY;


        if (lostCurrencyAmount > 0)
        {
            GameObject newLostCurrency = Instantiate(lostCurrencyPrefab, new Vector3(lostCurrencyX, lostCurrencyY), Quaternion.identity);
            newLostCurrency.GetComponent<LostCurrencyController>().currency = lostCurrencyAmount;
        }

        lostCurrencyAmount = 0;

    }

    private IEnumerator LoadWithDelay(GameData _data)
    {
        yield return new WaitForSeconds(.1f);

        LoadCheckpoints(_data);
        LoadClosestCheckpoint(_data);
        LoadLostCurrency(_data);


    }

    public void SaveData(ref GameData _data)
    {
        _data.lostCurrencyAmount = lostCurrencyAmount;
        _data.lostCurrencyX = player.position.x;
        _data.lostCurrencyY = player.position.y;


        if (FindClosetCheckPoint() != null)
        {
            _data.closetCheckPointId = FindClosetCheckPoint().id;
        }
        

        Debug.Log("saving data");
        _data.checkPoints.Clear();

        foreach (CheckPoint checkPoint in checkPoints)
        {
            
            _data.checkPoints.Add(checkPoint.id, checkPoint.activationStaus);
           Debug.Log("CheckPoint ID: " + checkPoint.id + ", Activation Status: " + checkPoint.activationStaus);

            Debug.Log("检查点状态已保存");
        }
    }

    
    private CheckPoint FindClosetCheckPoint()
    {

        float closetDistance = Mathf.Infinity;
        CheckPoint closetCheckPoint = null;

        foreach(var checkPoint in checkPoints)
        {
            float distanceToCheckPoint = Vector2.Distance(player.position,checkPoint.transform.position);

            if (distanceToCheckPoint < closetDistance && checkPoint.activationStaus == true)
            { 
                closetDistance = distanceToCheckPoint;
                closetCheckPoint = checkPoint;
            }
        }

        return closetCheckPoint;


    }
}
