using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;
    [SerializeField] public float xPosition;
    [SerializeField] public float yPosition;
    [SerializeField] public float zPosition;


void Start()
{
    controller = GetComponent<CharacterController>();
    controller = GetComponent<CharacterController>();

    // Set the starting position
    transform.position = new Vector3(xPosition, yPosition, zPosition);
}

void Update()
{
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");


    Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;


    controller.Move(moveDirection * moveSpeed * Time.deltaTime);
}
}
