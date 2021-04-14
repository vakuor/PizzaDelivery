using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{/*
    [SerializeField]
    private float speedModifier = 2.5f;
    [SerializeField]
    private float rotationSpeedModifier = 2f;

    [SerializeField]
    private LayerMask navMeshMask;
    private void Start() {
        Debug.Log(transform.rotation);
        Debug.Log(transform.rotation.x+" "+transform.rotation.y+" "+transform.rotation.z);
        Debug.Log(Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
        
        //transform.Rotate(-transform.localRotation.x, 0, 0);
    }
    void Update()
    {*/
        //transform.Rotate(transform.rotation.ToAxisAngle());
        /*float x = transform.localRotation.x;
        transform.Rotate(-x, 0, 0);
        */
        //Vector3 v = transform.TransformPoint(Vector3.forward).normalized ;
        //v.y = 0;
        //transform.Translate(v * Time.deltaTime * speedModifier, Space.World);
        /*
        transform.Translate(GameInputManager.MoveAxis() * Time.deltaTime * speedModifier);
        transform.Rotate(x, 0, 0);*/
        
/*
        t.position = Vector3.zero;
        t.Translate(GameInputManager.MoveAxis() * Time.deltaTime * speedModifier);
        transform.position+=t.position;*/

        //transform.Rotate(new Vector3(0, GameInputManager.RotateAxis() * Time.deltaTime * rotationSpeedModifier, 0), Space.World);
        
    //}
}