using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;

public class MainCharacter : Character
{

    #region PRIVATE_PROPERTIES
    public float _mana = 100;
    public float maxMana = 100;
    private float manaRestoreInterval = 1;
    private float manaRestoreRate = 5;
    private float _mouseSensitivity;
    private float timeBtwShots = 0.2f;
    private float timeOfLastShot = 0;
    public CharacterStats Stats => _characterStats;
    [SerializeField] private CharacterStats _characterStats;
    public float MaxHealth => _characterStats.MaxHealth;
    public float JumpHeight => _characterStats.JumpHeight;
    public float MovementSpeed => _characterStats.MovementSpeed;
    [SerializeField] private AudioSource audioSource;
    #endregion
    
    #region KEY_BINDINGS

    [SerializeField] private KeyCode _attack = KeyCode.Mouse0;
    [SerializeField] private KeyCode _teleport = KeyCode.Mouse1;
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
    private CmdTeleport _cmdTeleport;

    private void InitMovementCommands() {
        _cmdMovement = new CmdMovement(transform, controller, MovementSpeed);
        _cmdJump = new CmdJump(controller, _jump, JumpHeight);
        _cmdShoot = new CmdShoot(_wand);
        _cmdTeleport = new CmdTeleport(_wand);
    }

    #endregion

    private void Jump() {
        if (Input.GetKeyDown(_jump)) {
            EventQueueManager.instance.AddCommand(_cmdJump);
        }
        else
            _cmdJump.UpdateSpeed();
    }

    private void RestoreMana() {
        if (_mana < maxMana) {
            _mana += manaRestoreRate;
            EventsManager.instance.SpellCast(_mana, maxMana);
        }
    }

    public float getMana() => _mana;
    public void setMana(float mana) {
        _mana = mana;
    }

    public float getMaxMana() => maxMana;

    #region UNITY_EVENTS
 
    void Start() {
        base.stats = _characterStats;
        base.Start();
        _health = MaxHealth;
        Debug.Log(EventsManager.instance);
        controller = GetComponent<CharacterController>();

        InvokeRepeating("RestoreMana", manaRestoreInterval, manaRestoreInterval);
        InitMovementCommands();
    }
        
    void Update() {
        if (Input.GetKey(_moveForward) || Input.GetKey(_moveBackward) || Input.GetKey(_moveRight) || Input.GetKey(_moveLeft)) EventQueueManager.instance.AddCommand(_cmdMovement);
        
        Jump();
        
        float spellDamage = _wand.SpellPrefab.GetComponent<SimpleSpell>().Damage;
        if (Input.GetKeyDown(_attack)) {
            if (_mana > spellDamage && Time.time - timeOfLastShot >= timeBtwShots) {
                _mana -= spellDamage;
                _wand.spellAnimator.SetTrigger("SimpleShoot");
                EventQueueManager.instance.AddCommand(_cmdShoot);
                EventsManager.instance.SpellCast(_mana, maxMana);
                timeOfLastShot = Time.time;
            } 
        }
        if(Input.GetMouseButton(1)){
            RaycastHit hit = new RaycastHit();
            if (_wand.aimTeleport(out hit)) {
                GameObject lightCursor = GameObject.Find("/Teleport Cursor");
                Debug.Log(lightCursor);
                if (lightCursor == null) {
                    lightCursor = new GameObject("Teleport Cursor");
                    Light lightComp = lightCursor.AddComponent<Light>();
                    lightComp.intensity = 2;
                }
                Vector3 hitPosition = hit.point;
                hitPosition.y += 3;
                lightCursor.transform.position = hitPosition;
            } else {
                GameObject lightCursor = GameObject.Find("/Teleport Cursor");
                Destroy(lightCursor);
            }
        }
        float teleportManaCost = _wand.TeleportCost;
        if (Input.GetMouseButtonUp(1)) {
            GameObject lightCursor = GameObject.Find("/Teleport Cursor");
            if (lightCursor != null)
                Destroy(lightCursor);
            if (_mana > teleportManaCost && Time.time - timeOfLastShot >= timeBtwShots) {
                EventQueueManager.instance.AddCommand(_cmdTeleport);
                _wand.spellAnimator.SetTrigger("Teleport");
                timeOfLastShot = Time.time;
            }
        }
        // if (Input.GetKeyDown(_teleport)) {
        //     if (_mana > teleportManaCost && Time.time - timeOfLastShot >= timeBtwShots) {
        //         EventQueueManager.instance.AddCommand(_cmdTeleport);
        //         _wand.spellAnimator.SetTrigger("Teleport");
        //         timeOfLastShot = Time.time;
        //     }
        // }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Enemy") {
            EventQueueManager.instance.AddCommand(new CmdApplyDamage(this, col.gameObject.GetComponent<Blob>().Damage));
            StatsManager.instance.addHitsTaken();
            EventsManager.instance.SoundEffect(audioSource.clip);
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "PowerUp") {
            if (_health == MaxHealth) return;
            _health += 10;
            _health = (MaxHealth - _health > 0) ? _health : MaxHealth;
            EventsManager.instance.CharacterLifeChange(_health, MaxHealth);
            StatsManager.instance.addCoinsPicked();
        }

        if (col.gameObject.tag == "EndGame") {
            EventsManager.instance.EventGameOver(true);
        }
    }
    #endregion
}
