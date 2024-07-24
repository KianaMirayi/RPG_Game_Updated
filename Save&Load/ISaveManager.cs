using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager //接口
{

    void LoadData(GameData _data);
    void SaveData(ref GameData _data); //按引用传递参数将对_data的修改会影响到方法外部的GameData类成员变量

}
