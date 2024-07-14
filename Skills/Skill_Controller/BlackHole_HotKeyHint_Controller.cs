using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackHole_HotKeyHint_Controller : MonoBehaviour
{
    private SpriteRenderer HotKeySr;
    private KeyCode MyHotKey;
    private TextMeshProUGUI MyText;

    public Transform EnemyTransform;
    public BlackHole_Skill_Controller BlackHole;

    public void SetUpHotKey(KeyCode _myHotKey, Transform _enemyTransform, BlackHole_Skill_Controller _blackHole)
    {
        HotKeySr = GetComponent<SpriteRenderer>();
        MyText = GetComponentInChildren<TextMeshProUGUI>();

        MyHotKey = _myHotKey;
        MyText.text = _myHotKey.ToString();

        EnemyTransform = _enemyTransform;
        BlackHole = _blackHole;

    }


    private void Update()
    {
        if (Input.GetKeyDown(MyHotKey))
        {
            BlackHole.AddEnemyToList(EnemyTransform);

            MyText.color = Color.clear;
            HotKeySr.color = Color.clear;
             
        }
    }
}
