using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInputManager
{
    public static bool JumpPressed(){
        return Input.GetButtonDown("Jump");
    }
    public static Vector3 MoveAxis(){
        return new Vector3(MoveHorizontal(), 0, MoveVertical());
    }
    public static float RotateAxis(){
        float result = 0.0f;
        result += Input.GetAxis("CameraRotation");
        return Mathf.Clamp(result, -1.0f, 1.0f);
    }
    private static float MoveHorizontal(){
        float result = 0.0f;
        result += Input.GetAxis("Horizontal");
        return Mathf.Clamp(result, -1.0f, 1.0f);
    }

    private static float MoveVertical(){
        float result = 0.0f;
        result += Input.GetAxis("Vertical");
        return Mathf.Clamp(result, -1.0f, 1.0f);
    }
}
