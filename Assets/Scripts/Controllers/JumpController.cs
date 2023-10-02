using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour, IJumpable
{
    public float JumpHeight => _jumpHeight;
    private float _jumpHeight = 3.5f;
    private float _jumpSpeed;
    private float gravity = -9.81f;
    private float gravityScale = 1.2f;
    [SerializeField] public CharacterController _controller;
    [SerializeField] private KeyCode _jump = KeyCode.Space;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _controller.minMoveDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump() {
        if (Input.GetKeyDown(_jump) && _controller.isGrounded) {
            _jumpSpeed = Mathf.Sqrt(_jumpHeight * -2f * (gravity * gravityScale));
        }
        _jumpSpeed += gravity * gravityScale * Time.deltaTime;
        _controller.Move(new Vector3(0, _jumpSpeed, 0) * Time.deltaTime);
    }
}
