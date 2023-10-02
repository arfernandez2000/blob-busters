using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class MainCharacter : Character
{

    #region PRIVATE_PROPERTIES
    private int _mana = 100;
    
    private float _mouseSensitivity;
    private MovementController _movementController;
    private JumpController _jumpController;
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

    #region COMMANDS
    private CmdMovement _cmdMovement;
    private CmdJump _cmdJump;
    private CmdShoot _cmdShoot;

    private void InitMovementCommands() {
        _cmdMovement = new CmdMovement(_movementController);
        _cmdJump = new CmdJump(_jumpController);
        _cmdShoot = new CmdShoot(_wand);
    }

    #endregion

    private void Jump() {
        if (Input.GetKeyDown(_jump)) 
            EventQueueManager.instance.AddCommand(_cmdJump);
        else
            _cmdJump.Do();
    }

    #region UNITY_EVENTS
 
    // Start is called before the first frame update
    void Start()
    {
        _maxHealth = 100;
        _health = _maxHealth;
        // EventsManager.instance.CharacterLifeChange(_health, _maxHealth);
        controller = GetComponent<CharacterController>();
        _movementController = GetComponent<MovementController>();
        _jumpController = GetComponent<JumpController>();

        InitMovementCommands();
    }
        
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(_moveForward) || Input.GetKey(_moveBackward) || Input.GetKey(_moveRight) || Input.GetKey(_moveLeft)) EventQueueManager.instance.AddCommand(_cmdMovement);
        
        Jump();
        
        if (Input.GetKeyDown(_attack)) EventQueueManager.instance.AddCommand(_cmdShoot);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Enemy") {
            EventQueueManager.instance.AddCommand(new CmdApplyDamage(this, col.gameObject.GetComponent<Blob>().Damage));
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

        if (col.gameObject.tag == "EndGame") {
            Debug.Log("Choco con el cofre");
            EventsManager.instance.EventGameOver(true);
        }
    }
    #endregion
}
