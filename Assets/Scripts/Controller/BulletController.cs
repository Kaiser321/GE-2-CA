using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 100;
    public int time = 5;

    void Start()
    {
        Destroy(this.gameObject, time);
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);

        GameObject[] ships = GameObject.FindGameObjectsWithTag("Tie-Fighter");

        foreach (GameObject ship in ships)
        {
            if (Vector3.Distance(transform.position, ship.transform.position) < 2.5)
            {
                ship.GetComponent<ShipController>().health -= 1;
                Destroy(gameObject);
                break;
            }
        }
    }
}
