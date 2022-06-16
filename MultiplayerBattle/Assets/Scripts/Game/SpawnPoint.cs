using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkStartPosition))]
public class SpawnPoint : NetworkBehaviour
{
    [SerializeField] private Transform _characterTransform;

    private void Awake()
    {
        var characterModelTransform = _characterTransform.GetChild(0);
        transform.position += new Vector3(0, characterModelTransform.localScale.y);
    }
}
