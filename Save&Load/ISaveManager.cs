using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager //�ӿ�
{

    void LoadData(GameData _data);
    void SaveData(ref GameData _data); //�����ô��ݲ�������_data���޸Ļ�Ӱ�쵽�����ⲿ��GameData���Ա����

}
