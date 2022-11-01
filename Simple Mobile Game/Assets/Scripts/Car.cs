using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float speedGainOverTime = 0.1f;
    [SerializeField] float turnSpeed = 200f;

    bool paused = true;

    int steerValue;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        transform.Rotate(0f, steerValue * turnSpeed * Time.deltaTime, 0f);

        speed += speedGainOverTime * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {   
            SceneManager.LoadScene(0);
        }
    }


    // input system from ui
    public void Steer(int Value)
    {
        steerValue = Value;
    }
}
