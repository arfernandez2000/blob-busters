using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpell : MonoBehaviour, ISpell
{
    public float Speed => 15;

    public float Damage => 10;

    public float ManaCost => throw new System.NotImplementedException();

    public float Lifetime => 3f;

    public float Cooldown => throw new System.NotImplementedException();

    #region PRIVATE_PROPERTIES
    
    #endregion

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void Travel() => transform.position += Vector3.forward * Time.deltaTime * Speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Travel();
    }
}
