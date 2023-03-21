using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Сохранение
/// </summary>
public class Storage
{
    private string _filePath = default;
    private BinaryFormatter _binaryFormatter = default;

    public Storage()
    {
        _filePath = Application.persistentDataPath + "/GameResources.save";
        InitBinaryFormatter();
    }

    private void InitBinaryFormatter() => _binaryFormatter = new BinaryFormatter();

    /// <summary>
    /// Загрузить
    /// </summary>
    /// <returns></returns>
    public object Load(object saveDataByDefault)
    {
        if (!File.Exists(_filePath))
        {
            if (saveDataByDefault != null)
            {
                Save(saveDataByDefault);
            }
            return saveDataByDefault;
        }

        var file = File.Open(_filePath, FileMode.Open);

        var savedData = _binaryFormatter.Deserialize(file);
        file.Close();

        return savedData;
    }

    /// <summary>
    /// Сохранить
    /// </summary>
    public void Save(object saveData)
    {
        var file = File.Create(_filePath);
        _binaryFormatter.Serialize(file, saveData);
        file.Close();
    }
}
