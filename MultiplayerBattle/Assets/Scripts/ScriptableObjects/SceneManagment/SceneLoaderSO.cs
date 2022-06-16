using System;
using UnityEngine;

[CreateAssetMenu]
public class SceneLoaderSO : ScriptableObject
{
    public event Action<float> OnThereIsWinner;

    public void ThereIsWinnerRaise(float seconds)
    {
        OnThereIsWinner?.Invoke(seconds);
    }
}
