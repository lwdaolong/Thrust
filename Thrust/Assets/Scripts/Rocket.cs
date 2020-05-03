using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audio;

    bool isTurning;
    float deltaRotation;

    [SerializeField] float thrustforce;
    [SerializeField] float turnSpeed;


    enum State { alive, dying, transcending};
    State playerState = State.alive;



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
        if (playerState.Equals(State.alive))
        {
            Rotate();
        }
        Thrust();
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space) && playerState.Equals(State.alive))
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

        if (playerState != State.alive)
            return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                {
                    print("Friendly");
                    break;
                }
            case "Finish":
                {
                    playerState = State.transcending;
                    Invoke("LoadNextLevel", 1f);
                    break;
                }
            default:
                {
                    playerState = State.dying;
                    Invoke("Restart", 1f);
                    break;
                }

        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
}
