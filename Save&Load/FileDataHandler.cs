using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler //从文件的路径和名字读取数据
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
        // 将数据目录路径和文件名组合成完整的文件路径
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            // 确保目录存在。如果目录不存在，将创建目录
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // 将 GameData 对象序列化为 JSON 格式字符串
            string dataToStore = JsonUtility.ToJson(_data, true);

            if (encryptData)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            // 使用 FileStream 创建或覆盖指定路径的文件
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                // 创建一个 StreamWriter 来写入文件流
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    // 将 JSON 格式字符串写入文件
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("尝试保存数据至该文件时发生错误： " + fullPath + "\n" + e);
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
                Debug.LogError("尝试从该文件读取数据时发生错误： " + fullPath + "\n" + e);
            }
        }

        return loadData;
    }

    public void Delete() //删除保存的文件
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath); 
        }
    }

    private string EncryptDecrypt(string _Data) //数据加密
    {
        string modifiedData = "";

        for (int i = 0; i < _Data.Length; i++)
        {
            modifiedData += (char)(_Data[i] ^ codeWord[i % codeWord.Length]);
        }
        
        return modifiedData;
    }

}
