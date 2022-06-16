using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class WinUI : NetworkBehaviour
{
    [SerializeField] 
    private GameProcces _gameProcces;
    [SerializeField] 
    private Text _winText;

    public GameObject WinTextObject => _winText.gameObject;

    private void OnEnable()
    {
        WinTextObject.SetActive(false);
        _gameProcces.OnThereIsWinner += ShowWinText;
    }

    private void OnDisable()
    {
        _gameProcces.OnThereIsWinner -= ShowWinText;
    }

    [ClientRpc]
    private void ShowWinText(string winerName)
    {
        WinTextObject.SetActive(true);
        _winText.text = winerName + " Победил!";
    }
}
