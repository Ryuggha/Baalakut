using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
