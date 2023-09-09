using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWand
{
    GameObject SpellPrefab { get; }
    Transform SpellContainer { get; }
    void Shoot();
}
