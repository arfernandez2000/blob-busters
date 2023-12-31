using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdJump : ICommand
{
    private float _jumpHeight;
    private float _jumpSpeed;
    private float _gravity = -9.81f;
    private float _gravityScale = 1.2f;
    private CharacterController _controller;
    private KeyCode _jump;

    public CmdJump(CharacterController controller, KeyCode jump, float jumpHeight) {
        _controller = controller;
        _jump = jump;
        _jumpHeight = jumpHeight;
        _controller.minMoveDistance = 0;
    }

    public void Do() {
        if (_controller.isGrounded) {
            _jumpSpeed = Mathf.Sqrt(_jumpHeight * -2f * (_gravity * _gravityScale));
            UpdateSpeed();
        }
    }

    public void UpdateSpeed() {
        _jumpSpeed += _gravity * _gravityScale * Time.deltaTime;
        _controller.Move(new Vector3(0, _jumpSpeed, 0) * Time.deltaTime);
    }
}
