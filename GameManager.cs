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
        checkPoints = FindObjectsOfType<CheckPoint>(); //ע��������Ϸ�����еļ���
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


    public void RestartScene()  //���¿�ʼ��Ϸ
    {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data)
    {
        StartCoroutine(LoadWithDelay(_data));

        //Debug.Log("���ؼ�������");
        //Debug.Log("CheckPoints in data: " + _data.checkPoints.Count);

        //ClosetCheckPointLoaded = _data.closetCheckPointId;
        //Debug.Log("��������ѷ���" + ClosetCheckPointLoaded);

        //foreach (KeyValuePair<string, bool> pair in _data.checkPoints)
        //{
        //    Debug.Log("���ڱ��������еļ�ֵ��");
        //    Debug.Log("Key: " + pair.Key + ", Value: " + pair.Value);

        //    foreach (CheckPoint checkPoint in checkPoints)
        //    {
        //        Debug.Log("׼����ÿһ����ֵ�Խ���ƥ��");
        //        Debug.Log("CheckPoint ID: " + checkPoint.id + ", Activation Status: " + checkPoint.activationStaus);


        //        if (checkPoint.id == pair.Key)
        //        {
        //            Debug.Log("ƥ���");

        //            //checkPoint.activationStaus = pair.Value;

        //            if (pair.Value == true)
        //            {
        //                Debug.Log("ƥ��ֵ");
        //                checkPoint.ActiveCheckPoint();
        //            }

        //            Debug.Log("�Ͼּ��������Ѽ���");
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

            Debug.Log("����״̬�ѱ���");
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
