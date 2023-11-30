using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    public void Save(int bestStage)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saves.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, bestStage);
        stream.Close();
    }

    public int Load()
    {
        string path = Application.persistentDataPath + "/saves.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            var data = (int)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Path error, no save date");
            return 0;
        }
    }
}
