using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayerState : PlayerState, IPlayerState
{
    private float _moveSpeed;

    public MovePlayerState(PlayerController playerController, Animator animator, UnityEngine.InputSystem.PlayerInput playerInput) : base(playerController, animator, playerInput)
    // :base (playerController, animator, playerInput)
    {
    }

    public void Enter()
    {
        // Move 애니메이션 실행
        _animator.SetBool(PlayerController.PlayerAniParamMove, true);  

        // _moveSpeed 초기화
        _moveSpeed = 0;      
    }

    public void Exit()
    {
        _animator.SetBool(PlayerController.PlayerAniParamMove, false);   
    }

    public void Update()
    {
        var moveVector = _playerInput.actions["Move"].ReadValue<Vector2>();
        if (moveVector != Vector2.zero)
        {
            // 캐릭터 회전
            Rotate(moveVector.x, moveVector.y);
        } 
        else
        {
            _playerController.SetState(PlayerController.EPlayerState.Idle);
        }

        var isRun = _playerInput.actions["Run"].IsPressed();

        if(isRun && _moveSpeed < 1f)
        {
            _moveSpeed += Time.deltaTime;
            _moveSpeed = Mathf.Clamp01(_moveSpeed);
        }
        else if (!isRun && _moveSpeed > 0)
        {
            _moveSpeed -= Time.deltaTime;
            _moveSpeed = Mathf.Clamp01(_moveSpeed);

        }

        _animator.SetFloat(PlayerController.PlayerAniParamMoveSpeed, _moveSpeed);
    }
}