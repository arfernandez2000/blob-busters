using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class MainCharacter : Character, IJumpable
{

    #region PRIVATE_PROPERTIES
    private int _mana = 100;
    private float _movementSpeed;
    private float _jumpSpeed;
    private float _jumpHeight;
    
    private float _mouseSensitivity;
    private MovementController _movementController;
    #endregion
    
    #region KEY_BINDINGS

    [SerializeField] private KeyCode _attack = KeyCode.Mouse0;
    [SerializeField] private KeyCode _jump = KeyCode.Space;
    [SerializeField] private CharacterController controller;
    [SerializeField] private SimpleWand _wand;
    [SerializeField] private KeyCode _moveForward = KeyCode.W;
    [SerializeField] private KeyCode _moveBackward = KeyCode.S;
    [SerializeField] private KeyCode _moveLeft = KeyCode.A;
    [SerializeField] private KeyCode _moveRight = KeyCode.D;
    #endregion

    #region MOVEMENT_COMMAND
    private CmdMovement _cmdMovement;

    private void InitMovementCommands() {
        _cmdMovement = new CmdMovement(_movementController);
    }

    #endregion

    #region IMOVEABLE

    [SerializeField] public float MovementSpeed => _movementSpeed;

    public void Move() => _cmdMovement.Do();

    #endregion

    #region IJUMPABLE
    [SerializeField] public float JumpHeight => _jumpHeight;
    public float gravity = -9.81f;
    public float gravityScale = 1;
    
    public void Jump()
    {
        if (Input.GetKeyDown(_jump))
        {
            _jumpSpeed = Mathf.Sqrt(_jumpHeight * -2f * (gravity * gravityScale));
        }
        _jumpSpeed += gravity * gravityScale * Time.deltaTime;
        controller.Move(new Vector3(0, _jumpSpeed, 0) * Time.deltaTime);
    }
    
    #endregion

    #region UNITY_EVENTS

    // Start is called before the first frame update
    void Start()
    {
        _maxHealth = 100;
        _health = _maxHealth;
        _movementSpeed = 10;
        _jumpHeight = 2f;
        // EventsManager.instance.CharacterLifeChange(_health, _maxHealth);
        controller = GetComponent<CharacterController>();
        _movementController = GetComponent<MovementController>();

        InitMovementCommands();
    }
        
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(_moveForward) || Input.GetKey(_moveBackward) || Input.GetKey(_moveRight) || Input.GetKey(_moveLeft)) _cmdMovement.Do();
        
        Jump();
        
        if (Input.GetKeyDown(_attack)) _wand.Shoot();
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Enemy") {
            TakeDamage(col.gameObject.GetComponent<Blob>().Damage);
            // EventsManager.instance.CharacterLifeChange(_health, _maxHealth);
            Debug.Log("Muriendo: " + _health);
        }
    }

    void OnTriggerEnter(Collider col) {
        Debug.Log("POWERUP: " + col.gameObject.tag);
        if (col.gameObject.tag == "PowerUp") {
            _health += 10;
            Debug.Log("Sano: " + _health);
            Destroy(col.gameObject);
        }
    }
    #endregion
}
