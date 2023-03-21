using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отображение ресурса
/// </summary>
public class ResourseView : MonoBehaviour
{
    /// <summary>
    /// Название ресурса
    /// </summary>
    public string ResourceName = default;

    [SerializeField]
    private Image _resourceImage = default;
    [SerializeField]
    private Text _resourceCountText = default;

    /// <summary>
    /// Обновить отображение
    /// </summary>
    public void UpdateView(Sprite sprite, string count)
    {
        _resourceImage.sprite = sprite;
        _resourceCountText.text = count;
    }
}
