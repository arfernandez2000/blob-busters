using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdApplyDamage : ICommand
{
    private IDamageable _damageable;
    private float _damage;

    public CmdApplyDamage(IDamageable damageable, float damage) {
        _damageable = damageable;
        _damage = damage;
    }

    public void Do() => _damageable.TakeDamage(_damage);

    // public void Undo() {

    // }
}
