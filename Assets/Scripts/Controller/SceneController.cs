using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Camera cam;
    public GameObject falcon;
    public bool FPSCamera;

    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new BigBattle());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            FPSCamera = !FPSCamera;
            if (FPSCamera)
            {
                cam.GetComponent<FPSCamera>().enabled = true;
            }
            else
            {
                cam.GetComponent<FPSCamera>().enabled = false;
            }
        }
    }

    public void DestoryObject(GameObject g)
    {
        Destroy(g);
    }
}

