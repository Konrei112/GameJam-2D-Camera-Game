using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public PlayerMovementStats MoveStats;
    [SerializeField] private Collider2D _feetColl;
    [SerializeField] private Collider2D _bodyColl;

    private Rigidbody2D _rb;

    //movement vars
    private Vector2 _moveVelocity;
    private bool _IsFacingRight;

    private RaycastHit2D _groundhit;
    private RaycastHit2D _headhit;
    private bool _isGrounded;
    private bool _bumpedHead;

    private void Awake()
    {
        _IsFacingRight = true;

        _rb= GetComponent<Rigidbody2D>();

    }



    #region Movement

    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
        if (moveInput != Vector2.zero)
        {
            TurnCheck(moveInput); 

            Vector2 targetVelocity = Vector2.zero;
            if (InputManager.RunIsHeld)
            {
                targetVelocity = new Vector2(moveInput.x, 0f) * MoveStats.MaxRunSpeed;
            }
            else { targetVelocity = new Vector2(moveInput.x, 0f) * MoveStats.MaxWalkSpeed; }

            _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            _rb.linearVelocity = new Vector2(_moveVelocity.x, _rb.linearVelocity.y);// Problem
        }
        else if (moveInput == Vector2.zero)
        {
            _moveVelocity= Vector2.Lerp(_moveVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            _rb.linearVelocity = new Vector2(_moveVelocity.x, _rb.linearVelocity.y);// Problem
        }
    }

    private void TurnCheck(Vector2 moveInput)
    {
        if (_IsFacingRight && moveInput.x < 0)
        {
            Turn(false);
        }
        else if (_IsFacingRight && moveInput.x < 0)
        {
            Turn(false);
        }
    }

    private void Turn(bool turnRight)
    {
        if (turnRight)
        {
            _IsFacingRight = true;
            transform.Rotate(0f, 180f, 0f);
        }
        else
        {
            _IsFacingRight = false;
            transform.Rotate(0f, -180f, 0f);
        }
    }

    #endregion
}



