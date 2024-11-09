using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Movement movement;
    //public Dash dash;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Health
    }

    // Update is called once per frame
    void Update()
    {
        movement.updateMovement(rb);
    }
}
