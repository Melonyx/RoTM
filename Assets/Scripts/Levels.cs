using System;
using UnityEngine;

[Serializable]
public class LevelInfo
{
    public string name;
    public string scene;
}

public class Levels : ScriptableObject
{
    public LevelInfo[] levels;
}
