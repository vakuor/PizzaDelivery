using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event", order = 61)]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        if(listeners != null)
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            //Debug.Log("Count: " + listeners.Count + " i: " + i);
            if(listeners[i]!=null) // TODO: kostil
                listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if(listener==null) { Debug.LogError("Listener is null!"); return; }
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if(listener==null) { Debug.LogError("Listener is null!"); return; }
        listeners.Remove(listener);
    }
}