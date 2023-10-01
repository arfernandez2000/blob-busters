using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdMovement : ICommand
{
    private IMovable _movable;

    public CmdMovement(IMovable movable) {
        _movable = movable;
    }

    public void Do() => _movable.Move();

    // public void Undo() {

    // }
}
