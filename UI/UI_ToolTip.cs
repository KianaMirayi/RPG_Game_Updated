using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ToolTip : MonoBehaviour
{
    [SerializeField] private float xLimit = 960;
    [SerializeField] private float yLimit = 540;

    [SerializeField] private float xOffset = 150;
    [SerializeField] private float yOffset = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void AdjustPositionForDescription()
    {
        Vector2 mousePosition = Input.mousePosition;
        //Debug.Log(mousePosition);

        float newXoffset = 0;
        float newYoffset = 0;

        if (mousePosition.x > xLimit)
        {
            newXoffset = -xOffset;
        }
        else
        {
            newXoffset = xOffset;
        }

        if (mousePosition.y > yLimit)
        {
            newYoffset = -yOffset;
        }
        else
        {
            newYoffset = yOffset;
        }

        transform.position = new Vector2(mousePosition.x + newXoffset, mousePosition.y + newYoffset); // 鼠标进入时界面适应鼠标位置
    }


    public void AdjustFontSize(TextMeshProUGUI _text) // 调整字体大小
    {
        if (_text.text.Length > 12)
        {
            _text.fontSize = _text.fontSize * 0.8f;
        }
    }
}
