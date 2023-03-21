using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������, ��������� �������
/// </summary>
public class PlayerResources : MonoBehaviour
{
    /// <summary>
    /// ��������� ���������� ��������
    /// </summary>
    public event Action onResourceCountChange = delegate { };

    /// <summary>
    /// ��������� �������
    /// </summary>
    public Dictionary<string, List<ResourceObject>> ResourceObjects { get; private set; } = new Dictionary<string, List<ResourceObject>>();

    /// <summary>
    /// �������� ������
    /// </summary>
    /// <param name="resourceObject"></param>
    public void AddResources(ResourceObject resourceObject)
    {
        if (ResourceObjects.ContainsKey(resourceObject.Name))
        {
            ResourceObjects[resourceObject.Name].Add(resourceObject);
        }
        else
        {
            ResourceObjects.Add(resourceObject.Name, new List<ResourceObject> { resourceObject });
        }


        onResourceCountChange();
    }
}
