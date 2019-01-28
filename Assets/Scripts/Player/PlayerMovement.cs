using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0F;
    public float rotationSpeed = 100.0F;
    Vector3 move;
    Transform head;
    Vector3 rotationBody;
    Vector3 rotationHead;
    Animator anim;
    CharacterController _controller;
    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        _controller = GetComponent<CharacterController>();
        rotationBody = transform.eulerAngles;
        head = transform.Find("Head");
        rotationHead = head.eulerAngles;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.TransformDirection(move);
        rotationHead.x -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        if (rotationHead.x < -60)
        {
            rotationHead.x = -60;
        }
        if (rotationHead.x > 60)
        {
            rotationHead.x = 60;
        }

        rotationBody.y += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        rotationHead.y += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        head.eulerAngles = rotationHead;
        transform.eulerAngles = rotationBody;
       
        if (move != Vector3.zero)
        {
            _controller.Move(move * Time.deltaTime * speed);
            _controller.Move(Physics.gravity * Time.deltaTime);
        }

        //slows down
        if (Input.GetKey(KeyCode.C))
        {
            _controller.Move(-(move * Time.deltaTime * speed) / 2);
        }

        //speeds up
        if (Input.GetKey(KeyCode.Space))
        {
            _controller.Move(move * Time.deltaTime * speed);
        }

        if (move.sqrMagnitude < Mathf.Epsilon) anim.SetBool("stops", true);
        else if (move.sqrMagnitude >= Mathf.Epsilon) anim.SetBool("stops", false);
    }
}
    
 