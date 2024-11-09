using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Movement movement;
    public Weapons weapon;
    //public Dash dash;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Health
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            weapon.Fire(movement.getMousePosition());
        } 
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            movement.startDash();
            Debug.Log("Dash Started in Player controller");
        }
        movement.updateMovement(rb);
    }
}
