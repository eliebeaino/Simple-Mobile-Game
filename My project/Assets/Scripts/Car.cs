using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float speedGainOverTime = 0.1f;
    [SerializeField] float turnSpeed = 200f;

    int steerValue;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        transform.Rotate(0f, steerValue * turnSpeed * Time.deltaTime, 0f);

        speed += speedGainOverTime * Time.deltaTime;
    }

    public void Steer(int Value)
    {
        steerValue = Value;
    }
}
