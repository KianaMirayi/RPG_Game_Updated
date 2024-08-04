using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string SceneName = "MainScene";  //MainScene������unity�е�ͬ��
    [SerializeField] private GameObject ContinueButton;
    [SerializeField] private UI_FadeScreen FadeScreen;

    private void Start()
    {
        if (SaveManager.instance.HasSavedData() == false)// ��û���ҵ��������Ϸ����ʱ���ء�������Ϸ����ť
        { 
            ContinueButton.SetActive(false);
        }
    }

    public void ContinueGame()
    { 
        //SceneManager.LoadScene(SceneName);

        StartCoroutine(LoadWithFade(1f));
    }

    public void NewGame()
    {
        SaveManager.instance.DeleteSavedData();
        //SceneManager.LoadScene(SceneName);
        StartCoroutine(LoadWithFade(1f));
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    IEnumerator LoadWithFade(float _delay)
    { 
        FadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(SceneName);
    }


    
}
