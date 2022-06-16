using System;
using UnityEngine;

public class GameProcces : MonoBehaviour
{
    [Header("Configurations")]
    [SerializeField] 
    private int _hittingCountToWin;
    [SerializeField] 
    private float _timeToReloadScene;

    [Header("ScriptableObjects")]
    [SerializeField] 
    private GameProccesSO _gameProccesSO;
    [SerializeField] 
    private SceneLoaderSO _sceneLoaderSO;

    public event Action<string> OnThereIsWinner;
 
    private void OnEnable()
    {
        _gameProccesSO.OnHittingWithJack += CheckWinner;
    }

    private void OnDisable()
    {
        _gameProccesSO.OnHittingWithJack -= CheckWinner;
    }

    private void CheckWinner(Character character, int hittCount)
    {
        if (hittCount >= _hittingCountToWin)
        {
            _sceneLoaderSO.ThereIsWinnerRaise(_timeToReloadScene);
            OnThereIsWinner?.Invoke(character.Name);
        }
    }
}
