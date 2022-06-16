using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField] 
    private PlayersNameSO _playersNameSO;
    [SerializeField] 
    private InputField _inputField;

    public void AddNewPlayer()
    {
        _playersNameSO.Add(_inputField.text);
    }
}
