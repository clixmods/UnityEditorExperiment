using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(ICharacter))]
public class CharacterMovement2D : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private ICharacter icharacter;
    private float _speed = 4;
    private Vector2 _velocity;
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        icharacter = GetComponent<ICharacter>();
        _speed = icharacter.CharacterSetting.Speed;
    }
    public void SetVelocity(Vector2 direction)
    {
        _velocity = direction * _speed;
        _rigidbody2D.velocity = _velocity;
    }
    public void SetVelocity(InputAction.CallbackContext context)
    {
        var vectorDirection = context.ReadValue<Vector2>();
        SetVelocity(vectorDirection);
    }
}
