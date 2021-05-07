using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Camera camera;
    public GameObject falcon;

    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new Scene8());
    }

   public void DestoryObject(GameObject g)
    {
        Destroy(g);
    }
}

