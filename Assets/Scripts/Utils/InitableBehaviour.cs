using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InitableBehaviour : MonoBehaviour
{
    public T InitBehaviour<T>() where T : MonoBehaviour
    {
        GameObject go = new GameObject();
        return null;
    }
}
