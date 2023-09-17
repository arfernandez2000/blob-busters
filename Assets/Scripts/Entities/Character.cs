using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{

    #region PRIVATE_PROPERTIES
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _health;

    #endregion

    #region IDAMAGEABLE

    public float MaxHealth => _maxHealth;
    public float Health => _health;    
    

    public void TakeDamage(float damage) {
        _health -= damage;

        if (_health <= 0) Die();
    }

    public void Die() {
        Destroy(gameObject);
    }

    #endregion

    #region UNITY_METHODS
    
    // Start is called before the first frame update
    void Start() {
        // _health = _maxHealth;
    }
    
    #endregion
}
