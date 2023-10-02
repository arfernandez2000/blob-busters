using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{

    #region PRIVATE_PROPERTIES
    public EntityStats Stats => stats;
    [SerializeField] protected EntityStats stats;
    [SerializeField] protected float _health;

    #endregion

    #region IDAMAGEABLE

    public float MaxHealth => stats.MaxHealth;
    public float Health => _health;    
    

    public void TakeDamage(float damage) {
        _health -= damage;

        if (_health <= 0) Die();
    }

    public void Die() {
        Debug.Log($"{name} died");

        if (name.Equals("Main Character")) EventsManager.instance.EventGameOver(false);
        else {
            Destroy(gameObject);
        }
    }

    #endregion

    #region UNITY_METHODS
    
    // Start is called before the first frame update
    protected void Start() {
        _health = MaxHealth;
    }
    
    #endregion
}
