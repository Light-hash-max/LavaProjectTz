using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Отображение ресурсов
/// </summary>
public class ResoursesView : MonoBehaviour
{
    [SerializeField]
    private ResourseView _resourseViewPrefab = default;
    [SerializeField]
    private PlayerResources _playerResources = default;

    private List<ResourseView> _resourseViews = new List<ResourseView>();
    private ResourseView _currentResourseView = default;

    private void Awake() => _playerResources.onResourceCountChange += UpdateView;

    private void OnDestroy() => _playerResources.onResourceCountChange -= UpdateView;

    private void AddView(string resourceName)
    {
        foreach (ResourseView resourseView in _resourseViews)
        {
            if (resourseView.ResourceName == resourceName)
            {
                resourseView.gameObject.SetActive(true);
                resourseView.UpdateView(_playerResources.ResourceObjects[resourceName][0].Icon, _playerResources.ResourceObjects[resourceName].Count.ToString());
                return;
            }
        }

        _currentResourseView = Instantiate(_resourseViewPrefab, transform);
        _currentResourseView.ResourceName = resourceName;
        _currentResourseView.UpdateView(_playerResources.ResourceObjects[resourceName][0].Icon, _playerResources.ResourceObjects[resourceName].Count.ToString());
        _resourseViews.Add(_currentResourseView);
    }

    private void RemoveView()
    {
        foreach (ResourseView resourseView in _resourseViews)
        {
            if (!_playerResources.ResourceObjects.ContainsKey(resourseView.ResourceName))
            {
                resourseView.gameObject.SetActive(false);
            }
            else if (_playerResources.ResourceObjects[resourseView.ResourceName].Count == 0)
            {
                resourseView.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateView()
    {
        foreach (string resourceName in _playerResources.ResourceObjects.Keys)
        {
            AddView(resourceName);
        }

        RemoveView();
    }
}
