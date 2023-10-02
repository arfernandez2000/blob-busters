using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdMovement : ICommand
{
    private Transform _transform;
    private CharacterController _controller;
    private float _movementSpeed;


    public CmdMovement(Transform transform, CharacterController controller, float movementSpeed) {
        _transform = transform;
        _controller = controller;
        _movementSpeed = movementSpeed;
    }

    public void Do() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = _transform.right * x + _transform.forward * z;

        _controller.Move(move * _movementSpeed * Time.deltaTime);
        _controller.SimpleMove(Physics.gravity);
    }

    // public void Undo() {

    // }
}
