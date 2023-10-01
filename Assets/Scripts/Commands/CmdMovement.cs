using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdMovement : ICommand
{
    private Transform _transform;
    private IMovable _movable;
    private Vector3 _direction;
    private float _speed;
    private CharacterController _controller;

    public CmdMovement(IMovable movable) {
        _movable = movable;
    }

    public void Do() => _movable.Move();

    // public void Undo() {

    // }
}
