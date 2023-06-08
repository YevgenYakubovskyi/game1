using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    private float _directionX = 0f;
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private AudioSource jumpSorceEffect;
    private enum MovementState{ idle, running, jumping, falling }

    private MovementState _state = MovementState.idle;

    // Start is called before the first frame update
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        _directionX = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(_directionX * _moveSpeed, _rb.velocity.y);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSorceEffect.Play();
            _rb.velocity = new Vector2(0, _jumpForce);
        }
        UpdateAnimationState();
        
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        if (_directionX > 0f)
        {
            state = MovementState.running;
            _spriteRenderer.flipX = false;
        }
        else if (_directionX < 0)
        {
            state = MovementState.running;
            _spriteRenderer.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (_rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (_rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        _anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        
    }
}
