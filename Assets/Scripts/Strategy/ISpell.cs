using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{
    float Speed { get; }
    float Damage { get; }
    float ManaCost { get; }
    float Lifetime { get; }
    float Cooldown { get; }

    void Travel();
    void Die();
}
