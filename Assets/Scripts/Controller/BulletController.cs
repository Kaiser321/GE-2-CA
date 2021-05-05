using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 100;

    void Start()
    {
        Destroy(this.gameObject, 5);
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
