using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Туториал фарма
/// </summary>
public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject _sourceTutorial = default;
    [SerializeField]
    private GameObject _spotTutorial = default;
    [SerializeField]
    private PlayerResources _playerResources = default;

    private void Start()
    {
        _playerResources.onResourceCountChange += ResourceTutorialStep;
        _playerResources.onResourceRemoving += SpotTutorialStep;

        _sourceTutorial.SetActive(true);
    }

    private void OnDestroy()
    {
        _playerResources.onResourceCountChange -= ResourceTutorialStep;
        _playerResources.onResourceRemoving -= SpotTutorialStep;
    }

    private void ResourceTutorialStep()
    {
        if (_sourceTutorial.activeSelf)
        {
            _sourceTutorial.SetActive(false);
            _spotTutorial.SetActive(true);
        }    
    }

    private void SpotTutorialStep()
    {
        if (_spotTutorial.activeSelf)
        {
            _spotTutorial.SetActive(false);
        }
    }
}
