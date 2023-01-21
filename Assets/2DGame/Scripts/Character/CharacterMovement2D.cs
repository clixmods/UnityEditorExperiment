using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterMovement2D : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private ICharacter icharacter;
    [Tooltip("The speed of movement, this value can be set in other script")]
    private float speed = 4;
    private Vector2 velocity;
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        icharacter = GetComponent<ICharacter>();
        speed = icharacter.CharacterSetting.Speed;
    }
    
    public void SetVelocity(Vector2 direction)
    {
        velocity = direction * speed;
        _rigidbody2D.velocity = velocity;
    }
    public void SetVelocity(InputAction.CallbackContext context)
    {
        Vector2 direction;
        direction = context.ReadValue<Vector2>();
        SetVelocity(direction);
    }
}
