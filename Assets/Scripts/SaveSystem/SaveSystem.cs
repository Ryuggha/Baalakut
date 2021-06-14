using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem 
{
    
    public static bool saveGame(GameData Data)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameData.kut";
        FileStream stream = new FileStream(path, FileMode.Create);

        
        binaryFormatter.Serialize(stream, Data);
        stream.Close();

        return true;
    }

    internal static bool DeleteData()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameData.kut";
        File.Delete(path);
        return true;
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/GameData.kut";

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

           
            GameData data = binaryFormatter.Deserialize(stream) as GameData;

            stream.Close();
            return data;
        }else
        {
           
            return null;
        }
    }
}
