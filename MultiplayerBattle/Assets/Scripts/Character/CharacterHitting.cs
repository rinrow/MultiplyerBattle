using System.Collections;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterHitting : NetworkBehaviour
{
    [SerializeField]
    private GameProccesSO _gameProccesSO;

    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void OnEnable()
    {
        _character.OnCharacterColliding += OnCharacterColliding;
    }

    private void OnDisable()
    {
        _character.OnCharacterColliding -= OnCharacterColliding;
    }

    private void OnCharacterColliding(Character second)
    {
        if (_character.IsOnTheJackState && !second.IsColorChanged)
        {
            print("OnCharacterColliding called");
            _character.IncreaseHittingCount(_character.name);
            _gameProccesSO.RaiseHitting(_character, _character.HittingCount);
            ChangeColorWithUnchanging(second, second.HittedColor);
        }
    }

    public void ChangeColorWithUnchanging(Character character, Color target)
    {
        RPCChangeColor(character, target, true);
    }

    [ClientRpc]
    private void RPCChangeColor(Character character, Color target, bool returnToPrevious)
    {
        character.ModelRenderer.material.color = target;
        if (returnToPrevious)
            StartCoroutine(ColorToPrevious(character, character.ColorReturningTime));
    }

    private IEnumerator ColorToPrevious(Character character, float seconds)
    {
        character.IsColorChanged = true;
        yield return new WaitForSeconds(seconds);
        RPCChangeColor(character, character.PreviousColor, false);
        character.IsColorChanged = false;
    }
}
