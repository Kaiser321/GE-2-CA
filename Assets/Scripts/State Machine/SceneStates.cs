using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Scene1 : State
{
    /*
     * In Scene 1 Falcon will fly towards the camera
     */
    Camera camera;
    GameObject falcon;
    GameObject sceneTarget;
    public override void Enter()
    {
        camera = owner.GetComponent<SceneController>().camera;
        falcon = owner.GetComponent<SceneController>().falcon;
        falcon.transform.position = new Vector3(0, 0, 0);
        camera.transform.position = new Vector3(0, 0, 90);
        camera.transform.eulerAngles = new Vector3(-5, 180, 0);
        sceneTarget = new GameObject("Target");
        sceneTarget.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 2, camera.transform.position.z + 10);
        falcon.GetComponent<Seek>().target = sceneTarget.transform.position;
        falcon.GetComponent<Seek>().enabled = true;

    }

    public override void Think()
    {
        if (Vector3.Distance(sceneTarget.transform.position, falcon.transform.position) <= 5)
        {
            owner.ChangeState(new Scene2());
        }

    }

    public override void Exit()
    {
        owner.GetComponent<SceneController>().DestoryObject(sceneTarget);
        falcon.GetComponent<Seek>().enabled = false;
    }
}

class Scene2 : State
{
    Camera camera;
    GameObject falcon;
    GameObject sceneTarget;
    public override void Enter()
    {
        camera = owner.GetComponent<SceneController>().camera;
        falcon = owner.GetComponent<SceneController>().falcon;

        camera.transform.position = new Vector3(0, falcon.transform.position.y+10, falcon.transform.position.z -10);
        camera.transform.eulerAngles = new Vector3(20, 0, 0);
        sceneTarget = new GameObject("Target");
        sceneTarget.transform.position = new Vector3(falcon.transform.position.x, falcon.transform.position.y, falcon.transform.position.z + 75);
        falcon.GetComponent<Seek>().target = sceneTarget.transform.position;
        falcon.GetComponent<Seek>().enabled = true;

    }

    public override void Think()
    {
        if (Vector3.Distance(sceneTarget.transform.position, falcon.transform.position) <= 5)
        {
            owner.ChangeState(new Pursuing());
        }

    }

    public override void Exit()
    {
        falcon.GetComponent<Seek>().enabled = false;
    }
}


//class Scene2 : State
//{
//    GameObject camera;
//    GameObject falcon;
//    GameObject sceneTarget;
//    public override void Enter()
//    {
//        camera = owner.GetComponent<SceneController>().camera;
//        falcon = owner.GetComponent<SceneController>().falcon;

//        camera.transform.position = new Vector3(0, falcon.transform.position.y + 10, falcon.transform.position.z - 10);
//        camera.transform.eulerAngles = new Vector3(20, 0, 0);
//        sceneTarget = new GameObject("Target");
//        sceneTarget.transform.position = new Vector3(falcon.transform.position.x, falcon.transform.position.y, falcon.transform.position.z + 75);
//        falcon.GetComponent<Seek>().target = sceneTarget.transform.position;
//        falcon.GetComponent<Seek>().enabled = true;

//    }

//    public override void Think()
//    {
//        if (Vector3.Distance(sceneTarget.transform.position, falcon.transform.position) <= 5)
//        {
//            owner.ChangeState(new Pursuing());
//        }

//    }

//    public override void Exit()
//    {
//        falcon.GetComponent<Seek>().enabled = false;
//    }
//}