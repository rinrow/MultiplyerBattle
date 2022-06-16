using System;
using UnityEngine;

[CreateAssetMenu]
public class GameProccesSO : ScriptableObject
{
    public event Action<Character, int> OnHittingWithJack;

    public void RaiseHitting(Character character, int hitCount)
    {
        OnHittingWithJack?.Invoke(character, hitCount);
    }
}
