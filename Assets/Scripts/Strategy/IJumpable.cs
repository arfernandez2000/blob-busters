using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJumpable
{
    float JumpHeight { get; }

    void Jump();
}
