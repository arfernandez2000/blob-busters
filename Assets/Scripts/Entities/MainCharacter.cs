using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Character
{

    #region PRIVATE_PROPERTIES
    private int _mana = 100;
    #endregion
    
    #region KEY_BINDINGS
    [SerializeField] private KeyCode _moveForward = KeyCode.W;
    [SerializeField] private KeyCode _moveBackward = KeyCode.S;
    [SerializeField] private KeyCode _moveLeft = KeyCode.A;
    [SerializeField] private KeyCode _moveRight = KeyCode.D;

    [SerializeField] private KeyCode _attack = KeyCode.Mouse0;
    #endregion

    #region UNITY_EVENTS

    // Start is called before the first frame update
    void Start()
    {
        _maxHealth = 100;
        _health = _maxHealth;
        _movementSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        // Move forward
        if (Input.GetKey(_moveForward)) Move(Vector3.forward);
        // Move backward
        if (Input.GetKey(_moveBackward)) Move(-Vector3.forward);
        // Move left
        if (Input.GetKey(_moveLeft)) Move(-Vector3.right);
        // Move right
        if (Input.GetKey(_moveRight)) Move(Vector3.right);
    }
    #endregion
}
