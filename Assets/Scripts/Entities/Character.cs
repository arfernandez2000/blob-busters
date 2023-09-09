using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{

    #region PRIVATE_PROPERTIES
    protected int _maxHealth;
    protected int _health;
    protected float _movementSpeed;
    protected float _mouseSensitivity;

    #endregion

    #region IDAMAGEABLE

    public int MaxHealth => _maxHealth;
    public int Health => _health;    
    

    public void TakeDamage(int damage) {
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
