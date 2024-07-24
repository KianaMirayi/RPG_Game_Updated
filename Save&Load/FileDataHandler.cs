using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler //���ļ���·�������ֶ�ȡ����
{
    private string dataDirPath = "";
    private string dataFileName = "";

    private bool encryptData = false;
    private string codeWord = "cannotencryptdata";

    public FileDataHandler(string _dataDirPath, string _dataFileName, bool encryptData)
    {
        this.dataDirPath = _dataDirPath;
        this.dataFileName = _dataFileName;
        this.encryptData = encryptData;
        
    }

    public void Save(GameData _data)
    {
        // ������Ŀ¼·�����ļ�����ϳ��������ļ�·��
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            // ȷ��Ŀ¼���ڡ����Ŀ¼�����ڣ�������Ŀ¼
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // �� GameData �������л�Ϊ JSON ��ʽ�ַ���
            string dataToStore = JsonUtility.ToJson(_data, true);

            if (encryptData)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            // ʹ�� FileStream �����򸲸�ָ��·�����ļ�
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                // ����һ�� StreamWriter ��д���ļ���
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    // �� JSON ��ʽ�ַ���д���ļ�
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("���Ա������������ļ�ʱ�������� " + fullPath + "\n" + e);
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToload = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToload = reader.ReadToEnd();
                    }
                }

                if (encryptData)
                {
                    dataToload = EncryptDecrypt(dataToload);
                }

                loadData = JsonUtility.FromJson<GameData>(dataToload);
            }
            catch (Exception e)
            {
                Debug.LogError("���ԴӸ��ļ���ȡ����ʱ�������� " + fullPath + "\n" + e);
            }
        }

        return loadData;
    }

    public void Delete() //ɾ��������ļ�
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath); 
        }
    }

    private string EncryptDecrypt(string _Data) //���ݼ���
    {
        string modifiedData = "";

        for (int i = 0; i < _Data.Length; i++)
        {
            modifiedData += (char)(_Data[i] ^ codeWord[i % codeWord.Length]);
        }
        
        return modifiedData;
    }

}
