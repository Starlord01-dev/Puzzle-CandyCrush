using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Level/Level Data")]

public class LevelSave : ScriptableObject
{

    private Dictionary<string, LevelData> LevelList;

    public LevelSave()
    {
        LevelList = new Dictionary<string, LevelData>();
    }

    public void saveLevel(LevelData level)
    {
        LevelList.Add(level.levelId, level);
    }

    public LevelData loadLevel(string levelName)
    {
        return LevelList[levelName];
    }

}
