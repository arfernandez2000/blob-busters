using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour, IMovable
{
    public float MovementSpeed => _movementSpeed;
    [SerializeField] private float _movementSpeed = 10;
    [SerializeField] public CharacterController _controller;

    void Start() {
        _controller = GetComponent<CharacterController>();
    }

    public void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * MovementSpeed * Time.deltaTime);
        _controller.SimpleMove(Physics.gravity);
    }
}
