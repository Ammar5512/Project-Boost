using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        } 
        float cycles = Time.time / period; //constantly increasing and decreasing over time

        const float tau = Mathf.PI * 2; //tau is a value which = 2 PIs = 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); //going from -1 to 1 the cycle

        movementFactor = (rawSinWave +1f) / 2f; //oscillating between 0-1 means two places
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;  
    }
}
