using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomUtils
{
    public static Transform FindMainParentByLayer(Transform child, int layer){
        if(child!=null && child.parent!=null)
        while(child.parent.gameObject.layer == layer){
            child = child.parent;
        }
        return child;
    }
}
