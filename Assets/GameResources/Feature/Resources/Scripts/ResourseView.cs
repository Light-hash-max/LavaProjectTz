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

    private int previousValue = 0;

    /// <summary>
    /// Обновить отображение
    /// </summary>
    public void UpdateView(Sprite sprite, int count)
    {
        if (count > previousValue && !LeanTween.isTweening(_resourceCountText.gameObject))
        {
            LeanTween.scale(_resourceCountText.gameObject, 1.4f * Vector3.one, 0.2f);
            LeanTween.scale(_resourceCountText.gameObject, 1f * Vector3.one, 0.2f).setDelay(0.2f);
        }

        previousValue = count;
        _resourceImage.sprite = sprite;
        _resourceCountText.text = count.ToString();
    }
}
