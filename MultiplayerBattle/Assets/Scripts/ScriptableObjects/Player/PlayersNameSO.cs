using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayersNameSO : ScriptableObject
{
    [SerializeField]
    private List<string> _playerNames = new List<string>();

    public string GetLast()
    {
        return _playerNames[_playerNames.Count - 1];
    }

    public void Add(string name)
    {
        _playerNames.Add(name);
    }
}
