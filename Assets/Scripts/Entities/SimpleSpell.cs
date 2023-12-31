using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpell : MonoBehaviour, ISpell
{
    public Vector3 forward;
    public float Speed => 30;

    [SerializeField] public float Damage => 20;

    public float ManaCost => throw new System.NotImplementedException();

    public float Lifetime => _lifeTime;

    public float Cooldown => throw new System.NotImplementedException();

    #region PRIVATE_PROPERTIES

    private float _lifeTime = 3f;
    
    #endregion

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Travel() {
        transform.position += forward * Time.deltaTime * Speed;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Travel();
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0) Die();
    }
}
