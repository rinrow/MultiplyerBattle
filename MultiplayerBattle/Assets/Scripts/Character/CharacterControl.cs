using System.Collections;
using UnityEngine;
using Mirror;

public class CharacterControl : NetworkBehaviour
{
    [Header("Moving Settings")]
    [SerializeField]
    private float _moovingSpeed;

    [Header("Jack Settings")]
    [SerializeField]
    private float _jackDistance;
    [SerializeField]
    private float _jackSpeed;

    [SerializeField]
    private Transform _cameraTransform;

    [SyncVar]
    private bool _isOnJackState;

    public bool IsOnJackState
    {
        get => _isOnJackState;

        [Command]
        private set => _isOnJackState = value;
    }

    public Vector3 CharacterPosition => transform.position;

    private void Start()
    {
        if (!isLocalPlayer)
            _cameraTransform.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var forwardDirection = _cameraTransform.forward * vertical * _moovingSpeed * Time.deltaTime;
        forwardDirection.y = 0;
        var rightDirection = _cameraTransform.right * horizontal * _moovingSpeed * Time.deltaTime;

        transform.Translate(rightDirection);
        transform.Translate(forwardDirection);

        if (Input.GetMouseButtonDown(0))
            Jack();
    }

    private void Jack()
    {
        StartCoroutine(ApplyImpulse());
    }

    private IEnumerator ApplyImpulse()
    {
        var delta = 0f;
        var targetPos = CharacterPosition + _cameraTransform.forward * _jackDistance;
        targetPos.y = CharacterPosition.y;
        var previousPos = CharacterPosition;

        IsOnJackState = true;
        while (delta < 1 / _jackSpeed)
        {
            yield return null;
            delta += Time.deltaTime;
            transform.position = Vector3.Lerp(previousPos, targetPos, delta * _jackSpeed);
        }
        IsOnJackState = false;
    }
}
