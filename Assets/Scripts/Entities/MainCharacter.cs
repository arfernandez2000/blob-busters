using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Character, IMovable, IJumpable
{

    #region PRIVATE_PROPERTIES
    private int _mana = 100;
    private float _movementSpeed;
    private float _jumpSpeed;
    private float _jumpHeight;
    
    private float _mouseSensitivity;
    #endregion
    
    #region KEY_BINDINGS

    [SerializeField] private KeyCode _attack = KeyCode.Mouse0;
    [SerializeField] private KeyCode _jump = KeyCode.Space;
    [SerializeField] private CharacterController controller;
    [SerializeField] private SimpleWand _wand;
    #endregion

    #region IMOVEABLE

    [SerializeField] public float MovementSpeed => _movementSpeed;


    public void Move(){
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * MovementSpeed * Time.deltaTime);
        controller.SimpleMove(Physics.gravity);
    }

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
        controller = GetComponent<CharacterController>();
    }
        
    // Update is called once per frame
    void Update()
    {
        // Move forward
        // if (Input.GetKey(_moveForward)) Move(Vector3.forward);
        // // Move backward
        // if (Input.GetKey(_moveBackward)) Move(-Vector3.forward);
        // // Move left
        // if (Input.GetKey(_moveLeft)) Move(-Vector3.right);
        // // Move right
        // if (Input.GetKey(_moveRight)) Move(Vector3.right);
        Move();
        Jump();
        
        if (Input.GetKeyDown(_attack)) _wand.Shoot();
    }
    #endregion
}
