
using Unity.VisualScripting;
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

    //jump vars
    public float VerticalVelocity { get; private set; }
    public bool _isJumping;
    public bool _isFastFalling;
    public bool _isFalling;
    public  float _fastFallTime;
    private float _fastFallReleaseSpeed;
    private int _numberOfJumpUsed;

    //apex vars
    private float _apexPoint;
    private float _timePastApexThreshold;
    private bool _isPastApexThreshold;

    // jump buffer vars
    private float _jumpBufferTimer;
    private bool _jumpReleasedDuringBuffer;

    //coyote time vars
    private float _coyoteTimer;

    public Animator animator;

    private void Awake()
    {
        _IsFacingRight = true;

        _rb= GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
         CountTimers();
        JumpCheck();
    }

    private void FixedUpdate()
    {
        Debug.Log("Is Facing Right"+_IsFacingRight);
        CollisionChecks();
        Jump();
        Debug.Log(_isGrounded);
        if (_isGrounded)
        {
            Move(MoveStats.GroundAcceleration, MoveStats.GroundDeceleration, InputManager.Movement);
        }
            
        else
        {
            Move(MoveStats.AirAcceleration, MoveStats.AirDeceleration, InputManager.Movement);
        }


        animator.SetFloat("Speed", Mathf.Abs(_rb.linearVelocity.x));
        animator.SetFloat("VSpeed", _rb.linearVelocityY);
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
    //Fine Tune Later

    private void TurnCheck(Vector2 moveInput)
    {
        if (_IsFacingRight && moveInput.x < 0)
        {
            Turn(false);
        }
        else if (!_IsFacingRight && moveInput.x > 0)
        {
            Turn(true);
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

    #region Jump

    public void JumpPad()
    {
        _isJumping = true;
        _isFalling = false;
        _isFastFalling = false;
        _jumpBufferTimer = 0f; 

       

            VerticalVelocity = MoveStats.InitialJumpVelocity;
     }
    private void JumpCheck()
    {
        if (InputManager.JumpWasPressed)
        {
            _jumpBufferTimer = MoveStats.JumpBufferTime;
            _jumpReleasedDuringBuffer = false;
        }


        if (InputManager.JumpWasReleased)
        {
            if (_jumpBufferTimer > 0f)
            {
                _jumpReleasedDuringBuffer = true;
            }

            if (_isJumping && VerticalVelocity > 0f)
            {
                if (_isPastApexThreshold)
                {
                    _isPastApexThreshold = false;
                    _isFastFalling = true;
                    _fastFallTime = MoveStats.TimeForUpwardsCancel;
                     VerticalVelocity = 0f;
                }
                else
                {
                    _isFastFalling = true;
                    _fastFallReleaseSpeed = VerticalVelocity;
                }


            }


        }
        if (_jumpBufferTimer > 0f && !_isJumping && (_isGrounded || _coyoteTimer > 0f))
        {
            initiateJump(1);

            if (_jumpReleasedDuringBuffer)
            {
                _isFalling = true;
                _fastFallReleaseSpeed = VerticalVelocity;


            }
        }
        else if (_jumpBufferTimer > 0f && _isJumping && _numberOfJumpUsed < MoveStats.NumberOfJumpsAllowed)
        {
            _isFastFalling = false;
            initiateJump(1);
        }

        else if (_jumpBufferTimer > 0f && _isJumping && _numberOfJumpUsed < MoveStats.NumberOfJumpsAllowed-1)
        {
           
            initiateJump(2);

            _isFastFalling = false;
        }
        //Landed
        if((_isJumping||_isFalling)&& _isGrounded&&VerticalVelocity<=0f)
        {
            _isJumping = false;
            _isFalling = false;
            _isFastFalling = false;
            _fastFallTime = 0f;
            _isPastApexThreshold = false;
            _numberOfJumpUsed = 0;

            VerticalVelocity = Physics2D.gravity.y;
        }

    }

    private void initiateJump(int numberOfJumpsUsed)
    {
        if (!_isJumping)
        { 
            _isJumping= true;
        }
        _jumpBufferTimer = 0f;
        _numberOfJumpUsed += numberOfJumpsUsed;
        VerticalVelocity = MoveStats.InitialJumpVelocity;

    }
    private void Jump()
    {
        if (_isJumping)
        {
            if (_bumpedHead)
            {
                _isFastFalling= true;
            }
            if (VerticalVelocity >= 0f)
            {
                _apexPoint = Mathf.InverseLerp(MoveStats.InitialJumpVelocity, 0f, VerticalVelocity);

                if (_apexPoint > MoveStats.ApexThreshold)
                {
                    if (!_isPastApexThreshold)
                    {
                        _isPastApexThreshold = true;
                        _timePastApexThreshold = 0f;
                    }

                    if (_isPastApexThreshold)
                    {
                        _timePastApexThreshold += Time.deltaTime;
                        if (_timePastApexThreshold < MoveStats.ApexHangTime)
                        {
                            VerticalVelocity = 0f;
                        }
                        else
                        {
                            VerticalVelocity = -0.01f;
                        }
                    }
                }

                else
                {
                    VerticalVelocity += MoveStats.Gravity * Time.deltaTime;
                    if (_isPastApexThreshold)
                    {
                        _isPastApexThreshold = false;
                    }
                }


            }


            else if (!_isFastFalling)
            {
                VerticalVelocity += MoveStats.Gravity * MoveStats.GravityOnReleaseMultiplayer * Time.fixedDeltaTime;
            }
            else if (VerticalVelocity < 0f)
            {
                if (!_isFalling)
                {
                    _isFalling = true;

                }
            }
        }


        if (_isFastFalling)
        {
            if (_fastFallTime >= MoveStats.TimeForUpwardsCancel)
            {
                VerticalVelocity += MoveStats.Gravity * MoveStats.GravityOnReleaseMultiplayer * Time.fixedDeltaTime;
            }
            else if (_fastFallTime < MoveStats.TimeForUpwardsCancel)
            {
                VerticalVelocity = Mathf.Lerp(_fastFallReleaseSpeed, 0f, (_fastFallTime / MoveStats.TimeForUpwardsCancel));
            }
            _fastFallTime += Time.fixedDeltaTime;

        }

        if(_isGrounded&&!_isJumping)
        {
            if (!_isFalling)
            {
                _isFalling = true;
            }
            VerticalVelocity += MoveStats.Gravity * Time.fixedDeltaTime;
        }


        VerticalVelocity = Mathf.Clamp(VerticalVelocity, -MoveStats.MaxFallSpeed, 50f);

        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, VerticalVelocity);

    }






    #endregion

    #region  Collision Checks
    private void IsGrounded()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.min.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x, MoveStats.GroundDetectionRayLength);

        _groundhit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, MoveStats.GroundDetectionRayLength, MoveStats.GroundLayer);
        if (_groundhit.collider != null)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
       
        #region Debug Visualization
        if (MoveStats.DebugShowIsGroundedBox)
        {
            Color rayColor;
            if (_isGrounded)
            {
                rayColor = Color.green;
            }
            else { rayColor = Color.red; }

            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * MoveStats.GroundDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x + boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * MoveStats.GroundDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y - MoveStats.GroundDetectionRayLength), Vector2.right * boxCastSize.x, rayColor);


        }
        
        #endregion
    }

    private void BumpedHead()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _bodyColl.bounds.max.x);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x*MoveStats.HeadWidth, MoveStats.HeadDetectionRayLength);

        _headhit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.up, MoveStats.HeadDetectionRayLength, MoveStats.GroundLayer);
        if (_headhit.collider != null)
        {
            _bumpedHead = true;
        }
        else
        {
            _bumpedHead = false;
        }

        #region Debug Visualization
        if (MoveStats.DebugShowHeadBumpBox)
        {
            float headwidth = MoveStats.HeadWidth;
            Color rayColor;
            if (_bumpedHead)
            {
                rayColor = Color.green;
            }
            else { rayColor = Color.red; }

            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2 * headwidth, boxCastOrigin.y), Vector2.down * MoveStats.HeadDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x + (boxCastSize.x / 2)*headwidth, boxCastOrigin.y), Vector2.down * MoveStats.HeadDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2 * headwidth, boxCastOrigin.y + MoveStats.HeadDetectionRayLength), Vector2.right * boxCastSize.x*headwidth, rayColor);


        }

        #endregion
    }

    private void CollisionChecks()
        {
            IsGrounded();
        BumpedHead();
        }
    #endregion

    #region Timers

    private void CountTimers()
    {
        _jumpBufferTimer -= Time.deltaTime;

        if (!_isGrounded)
        {
            _coyoteTimer -= Time.deltaTime;
        }
        else { _coyoteTimer = MoveStats.JumpCoyoteTime; }

    }

    #endregion


    #region Camera






    #endregion
}






