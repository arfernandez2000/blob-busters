using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpell : MonoBehaviour, ISpell
{
    public float Speed => throw new System.NotImplementedException();

    public float Damage => throw new System.NotImplementedException();

    public float ManaCost => throw new System.NotImplementedException();

    public float Lifetime => throw new System.NotImplementedException();

    public float Cooldown => throw new System.NotImplementedException();

    #region PRIVATE_PROPERTIES
    
    #endregion

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void Travel()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
