using System;
using System.Collections;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(CharacterControl))]
[RequireComponent(typeof(Rigidbody))]
public class Character : NetworkBehaviour
{
    [Header("Configurations")]

    [SerializeField]
    [SyncVar]
    private string _name;
    public string Name
    {
        get => _name;

        [Command(requiresAuthority = false)]
        set => _name = value;
    }

    [SerializeField]
    private float _colorReturningTime;
    public float ColorReturningTime => _colorReturningTime;

    [SerializeField]
    private Color _hittedColor;
    public Color HittedColor => _hittedColor;

    [SerializeField]
    private MeshRenderer _modelRenderer;
    public MeshRenderer ModelRenderer => _modelRenderer;


    [Header("ScriptableObjects")]
    [SerializeField]
    private PlayersNameSO _playersNameSO;

    private CharacterControl _characterControl;
    public bool IsOnTheJackState => _characterControl.IsOnJackState;

    private Color _previousColor;
    public Color PreviousColor => _previousColor;

    private bool _isNameInited = false;

    [SyncVar]
    private int _hittingCount;
    public int HittingCount
    {
        get => _hittingCount;

        [Command]
        private set => _hittingCount = value;
    }

    [SyncVar]
    private bool _isColorChanged;
    public bool IsColorChanged
    {
        get => _isColorChanged;

        [Command(requiresAuthority =false)]
        set => _isColorChanged = value;
    }

    public event Action<Character> OnCharacterColliding;

    private void Start()
    {
        if (!_isNameInited && isLocalPlayer)
        {
            Name = _playersNameSO.GetLast();
            _isNameInited = true;
        }
    }

    private void Awake()
    {
        _characterControl = GetComponent<CharacterControl>();
        _previousColor = ModelRenderer.material.color;
    }

    public void IncreaseHittingCount(string senderName)
    {
        HittingCount++;
    }

    //≈сли сталкнулись два рывка что дальше
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Character otherCharacter))
        {
            OnCharacterColliding?.Invoke(otherCharacter);
        }
    }
}
