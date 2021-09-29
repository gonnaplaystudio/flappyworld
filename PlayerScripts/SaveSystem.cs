using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SaveData(Data _data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.data";

        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData saveData = new SaveData(_data);

        formatter.Serialize(stream, saveData);

        stream.Close();
    }

    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/player.data";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData saveData = formatter.Deserialize(stream) as SaveData;

            stream.Close();

            return saveData;
        }
        else
        {
            Debug.Log("Error");
            return null;
        }
    }
}
