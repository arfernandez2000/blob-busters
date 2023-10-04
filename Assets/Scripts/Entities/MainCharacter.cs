using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;

public class MainCharacter : Character
{

    #region PRIVATE_PROPERTIES
    private float _mana = 100;
    private float maxMana = 100;
    private float _mouseSensitivity;
    private float timeBtwShots = 2;
    private float timeOfLastShot = 0;
    public CharacterStats Stats => _characterStats;
    [SerializeField] private CharacterStats _characterStats;
    public float MaxHealth => _characterStats.MaxHealth;
    public float JumpHeight => _characterStats.JumpHeight;
    public float MovementSpeed => _characterStats.MovementSpeed;
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
        _cmdMovement = new CmdMovement(transform, controller, MovementSpeed);
        _cmdJump = new CmdJump(controller, _jump, JumpHeight);
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
 
    void Start() {
        base.stats = _characterStats;
        base.Start();
        _health = MaxHealth;
        Debug.Log(EventsManager.instance);
        controller = GetComponent<CharacterController>();

        InitMovementCommands();
    }
        
    void Update() {
        if (Input.GetKey(_moveForward) || Input.GetKey(_moveBackward) || Input.GetKey(_moveRight) || Input.GetKey(_moveLeft)) EventQueueManager.instance.AddCommand(_cmdMovement);
        
        Jump();
        
        float spellDamage = _wand.SpellPrefab.GetComponent<SimpleSpell>().Damage;
        if (Input.GetKeyDown(_attack)) {
            if (_mana > spellDamage && Time.time - timeOfLastShot >= timeBtwShots) {
                _mana -= spellDamage;
                Debug.Log("MANA: " + _mana);
                EventQueueManager.instance.AddCommand(_cmdShoot);
                EventsManager.instance.SpellCast(_mana, maxMana);
                timeOfLastShot = Time.time;
            } 
        }
        
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Enemy") {
            EventQueueManager.instance.AddCommand(new CmdApplyDamage(this, col.gameObject.GetComponent<Blob>().Damage));
            EventsManager.instance.CharacterLifeChange(_health, MaxHealth);
            Debug.Log(EventsManager.instance);
            Debug.Log("Muriendo: " + _health);
        }
    }

    void OnTriggerEnter(Collider col) {
        Debug.Log("POWERUP: " + col.gameObject.tag);
        if (col.gameObject.tag == "PowerUp") {
            _health += 10;
            EventsManager.instance.CharacterLifeChange(_health, MaxHealth);
            EventsManager.instance.SoundEffect(col.gameObject.GetComponent<AudioSource>());
            Debug.Log("Sano: " + _health);
            // Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "EndGame") {
            Debug.Log("Choco con el cofre");
            EventsManager.instance.EventGameOver(true);
        }
    }
    #endregion
}
