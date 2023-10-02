using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWand : MonoBehaviour, IWand
{
    #region IWAND_PROPERTIES
    public GameObject SpellPrefab => _spellPrefab;

    public Transform SpellContainer => _spellContainer;
    #endregion

    #region PRIVATE_PROPERTIES
    [SerializeField] private GameObject _spellPrefab;
    [SerializeField] private Transform _spellContainer;
    #endregion

    #region UNITY_EVENTS
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region IWAND_METHODS
    public void Shoot() {
        GameObject bullet = Instantiate(_spellPrefab, transform.position, transform.rotation, SpellContainer);
        bullet.GetComponent<SimpleSpell>().forward = transform.parent.forward;
    }
    #endregion
}
