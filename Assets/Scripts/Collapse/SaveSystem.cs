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
            string path = board.path + "\\Assets\\Maps" + "/board" + board.CurrentBoardId + ".collapse";
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
        string path = board.path + "\\Assets\\Maps" + "/board" + board.LoadBoardId + ".collapse";
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
