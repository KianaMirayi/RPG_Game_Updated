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
    [SerializeField] private CheckPoint[] checkPoints;
    [SerializeField] private string ClosetCheckPointLoaded;

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
        

        //Debug.Log("Found " + checkPoints.Length + " check points.");

        foreach (var checkpoint in checkPoints)
    {
        //Debug.Log("CheckPoint ID: " + checkpoint.id);
    }


    }


    public void RestartScene()  //���¿�ʼ��Ϸ
    {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data)
    {
        //.Log("���ؼ�������");
        //Debug.Log("CheckPoints in data: " + _data.checkPoints.Count);

        foreach (KeyValuePair<string, bool> pair in _data.checkPoints)
        {
           // Debug.Log("���ڱ��������еļ�ֵ��");
            //Debug.Log("Key: " + pair.Key + ", Value: " + pair.Value);

            foreach (CheckPoint checkPoint in checkPoints)
            {
               // Debug.Log("׼����ÿһ����ֵ�Խ���ƥ��");
               // Debug.Log("CheckPoint ID: " + checkPoint.id + ", Activation Status: " + checkPoint.activationStaus);


                if (checkPoint.id == pair.Key)
                {
                   // Debug.Log("ƥ���");

                    checkPoint.activationStaus = pair.Value;

                    if (checkPoint.activationStaus)
                    {
                       // Debug.Log("ƥ��ֵ");
                        checkPoint.ActiveCheckPoint();
                    }
                    
                   // Debug.Log("�Ͼּ��������Ѽ���");
                }
            }
        }

        ClosetCheckPointLoaded = _data.closetCheckPointId;

        foreach (CheckPoint checkPoint in checkPoints)
        {
            if (_data.closetCheckPointId == checkPoint.id)
            { 
                PlayerManager.instance.player.transform.position = checkPoint.transform.position;
                
               // Debug.Log("11111111");
            }
        }

        //Debug.Log("GameManager Loaded");
    }

    public void SaveData(ref GameData _data)
    {
        _data.closetCheckPointId = FindClosetCheckPoint().id;

       // Debug.Log("saving data");
        _data.checkPoints.Clear();

        foreach (CheckPoint checkPoint in checkPoints)
        {
            
            _data.checkPoints.Add(checkPoint.id, checkPoint.activationStaus);
            //Debug.Log("CheckPoint ID: " + checkPoint.id + ", Activation Status: " + checkPoint.activationStaus);

            //Debug.Log("����״̬�ѱ���");
        }
    }

    private CheckPoint FindClosetCheckPoint()
    {

        float closetDistance = Mathf.Infinity;
        CheckPoint closetCheckPoint = null;

        foreach(var checkPoint in checkPoints)
        {
            float distanceToCheckPoint = Vector2.Distance(PlayerManager.instance.player.transform.position,checkPoint.transform.position);

            if (distanceToCheckPoint < closetDistance && checkPoint.activationStaus == true)
            { 
                closetDistance = distanceToCheckPoint;
                closetCheckPoint = checkPoint;
            }
        }

        return closetCheckPoint;


    }
}
