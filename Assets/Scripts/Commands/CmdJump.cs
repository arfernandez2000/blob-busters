using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdJump : ICommand
{
    private IJumpable _jumpable;

    public CmdJump(IJumpable jumpable) {
        _jumpable = jumpable;
    }

    public void Do() => _jumpable.Jump();

    // public void Undo() {

    // }
}
