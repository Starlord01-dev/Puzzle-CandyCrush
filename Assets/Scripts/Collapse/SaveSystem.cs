using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;

public static class SaveSystem
{
    private static List<string> SaveBoards = new List<string>();

    public static void SaveBoard(EditorBoard board)
    {
        if (!SaveBoards.Contains(board.CurrentBoardId))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = GameManager.instance.Path + "\\Maps" + "\\board" + board.CurrentBoardId + ".collapse";
            FileStream stream = new FileStream(path, FileMode.Create);

            LevelData data = new LevelData(board);

            formatter.Serialize(stream, data);
            stream.Close();
            SaveBoards.Add(board.CurrentBoardId);
        }
        else
        {
            Debug.LogError("This Id alredy exists");
        }

    }

    public static LevelData LoadLevel(EditorBoard board)
    {
        string path = GameManager.instance.Path + "\\Maps" + "\\board" + board.LoadBoardId + ".collapse";
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

    public static LevelData LoadLevel(LoadableBoard board)
    {
        string loadBoardId = board.GetLoadBoardId();
        string path = GameManager.instance.Path + "\\Maps" + "\\board" + loadBoardId + ".collapse";
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

    public static LevelData LoadLevel(string LevelId, string Boardpath)
    {
        var loadingRequest = UnityEngine.Networking.UnityWebRequest.Get(Boardpath + "\\Maps" + "\\board" + LevelId + ".collapse");
        loadingRequest.SendWebRequest();
        while (!loadingRequest.isDone)
        {
            if (loadingRequest.isNetworkError || loadingRequest.isHttpError)
            {
                break;
            }
        }
        if (loadingRequest.isNetworkError || loadingRequest.isHttpError)
        {

        }
        else
        {
            File.WriteAllBytes(Path.Combine(Application.persistentDataPath + LevelId + ".collapse"), loadingRequest.downloadHandler.data);
        }
        String path = Application.persistentDataPath + LevelId + ".collapse";
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
