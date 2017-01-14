using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class AnimalController : MonoBehaviour {

    //gravity
    public float gravity = -1f;

    private CharacterController2D _controller;
    private Vector3 _velocity;

    void Start() {

        _controller = GetComponent<CharacterController2D>();

    }

    void FixedUpdate() {

        //if grounded; set y velocity to 0
        if (_controller.isGrounded)
            _velocity.y = 0;

        //apply gravity with no acceleration
        _velocity.y = gravity * Time.deltaTime;

        //apply movement
        _controller.move(_velocity * Time.deltaTime);

        //set new velocity
        _velocity = _controller.velocity;

    }

}


