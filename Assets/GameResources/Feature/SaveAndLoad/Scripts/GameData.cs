using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Данный игры
/// </summary>
[Serializable]
public class GameData
{
    public ResourceData[] ResourceData = default;

    public GameData(ResourceData[] resourceData)
    {
        ResourceData = resourceData;
    }
}
