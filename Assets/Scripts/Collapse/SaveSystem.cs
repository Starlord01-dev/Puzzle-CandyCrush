using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveBoard(EditorBoard board)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/board.collapse";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(board);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static LevelData LoadLevel()
    {
        string path = Application.persistentDataPath + "/board.collapse";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close(); 

            return data;
        }
        else
        {
            Debug.LogError("Board not found in " + path);
            return null;
        }
    }
}
