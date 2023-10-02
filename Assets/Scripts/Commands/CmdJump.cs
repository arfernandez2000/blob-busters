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
    }

    public void Do() {
        if (Input.GetKeyDown(_jump) && _controller.isGrounded) {
            _jumpSpeed = Mathf.Sqrt(_jumpHeight * -2f * (_gravity * _gravityScale));
        }
        _jumpSpeed += _gravity * _gravityScale * Time.deltaTime;
        _controller.Move(new Vector3(0, _jumpSpeed, 0) * Time.deltaTime);
    }

    // public void Undo() {

    // }
}
