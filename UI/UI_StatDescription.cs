using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatDescription : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI statDescription;
    [SerializeField] private TextMeshProUGUI statStory;  //test


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowStatDescription(string _text)
    {
        statDescription.text = _text;

        //AdjustPositionForDescription();
        

        gameObject.SetActive(true);
    }

    public void HideStatDescription()
    {
        statDescription.text = " ";
        //statStory.text = " ";//test
        gameObject.SetActive(false);
    }

    public void ShowStatStory(string _text) //test
    {
        statStory.text = _text;//test
        gameObject.SetActive(true);
    }

    public void HideStatStory()
    {
        statStory.text = " ";//test
        gameObject.SetActive(false);
    }
}
