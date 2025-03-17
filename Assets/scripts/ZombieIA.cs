using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieIA : MonoBehaviour
{
    public GameObject Target;
    private float speed = 8f;
    void Start()
    {

    }
    void Update()
    {
        transform.LookAt(Target.gameObject.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }
}