using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] 
    private NetworkManager _networkManager;
    [SerializeField] 
    private SceneLoaderSO _sceneLoaderSO;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        _sceneLoaderSO.OnThereIsWinner += ReloadSceneAfterSeconds;
    }

    private void OnDisable()
    {
        _sceneLoaderSO.OnThereIsWinner -= ReloadSceneAfterSeconds;
    }

    private void ReloadSceneAfterSeconds(float seconds)
    {
        StartCoroutine(ReloadSceneAfterTime(seconds));
    }

    private IEnumerator ReloadSceneAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ReloadScene();
    }

    public void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        _networkManager.ServerChangeScene(currentScene.name);
    }
}
