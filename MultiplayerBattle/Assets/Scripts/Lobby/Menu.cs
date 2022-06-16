using Mirror;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private NetworkManager _networkManager;

    public void StartHost()
    {
        _networkManager.StartHost();
    }

    public void SetIp(string ip)
    {
        _networkManager.networkAddress = ip;
    }

    public void Join()
    {
        _networkManager.StartClient();
    }
}
