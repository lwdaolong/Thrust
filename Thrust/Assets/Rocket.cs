using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audio;


    public float thrustforce;
    public float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
          //  print("can't turn both ways");

        }
        else if (Input.GetKey(KeyCode.D))
        {
            // print("right");
            transform.Rotate(turnSpeed * Time.deltaTime * Vector3.back);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //print("left");
            transform.Rotate(turnSpeed * Time.deltaTime * Vector3.forward);
        }


        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(thrustforce * Time.deltaTime * Vector3.up);
            //print("Thrusting");
            if (!audio.isPlaying)
                audio.Play();
        }
        else
        {
            audio.Stop();
        }

    }

}
