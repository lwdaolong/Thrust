using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audio;

    bool isTurning;
    float deltaRotation;

    [SerializeField] float thrustforce;
    [SerializeField] float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        float deltaRotation = 0;
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
 
    }

    private void Thrust()
    {
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

    private void Rotate()
    {
        rigidbody.freezeRotation = true;



        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            //  print("can't turn both ways");
            deltaRotation = 0;


        }
        else if (Input.GetKey(KeyCode.D))
        {
            // print("right");
            deltaRotation += turnSpeed * Time.deltaTime;
            transform.Rotate(turnSpeed * Time.deltaTime * Vector3.back);
         //   print(deltaRotation);
            if (deltaRotation >= 360)
            {
                print("Nice Flip!");
                deltaRotation = 0;
            }

        }
        else if (Input.GetKey(KeyCode.A))
        {
            //print("left");
            deltaRotation += turnSpeed * Time.deltaTime;
            transform.Rotate(turnSpeed * Time.deltaTime * Vector3.forward);
            //  print(deltaRotation);

            if (deltaRotation >= 360)
            {
                print("Nice Flip!");
                deltaRotation = 0;
            }

        }
        else
        {
            deltaRotation = 0;
        }
        isTurning = false;
        rigidbody.freezeRotation = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                {
                    print("Friendly");
                    break;
                }

            default:
                {
                    print("dead");
                    break;
                }

        }
    }

}
