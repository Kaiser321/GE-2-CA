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
        GetComponent<StateMachine>().ChangeState(new Scene1());
    }

   public void DestoryObject(GameObject g)
    {
        Destroy(g);
    }
}

