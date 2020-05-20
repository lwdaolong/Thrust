using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movement =  new Vector3(10f,10f,10f);
    [SerializeField] float period = 2f;

    [Range(0,1)]
    [SerializeField]
    float movementFactor;

    private Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles;
        if(period != 0)
        {
             cycles = Time.time / period; //grows continually from 0

        }
        else
        {
             cycles = Time.time / 1;
        }

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave/2f + .5f;
       // movementFactor = rawSinWave / 2f + 0.5f;


        Vector3 offset = movement * movementFactor;
        transform.position = startingPos + offset;
    }
}
