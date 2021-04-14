using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : InitableBehaviour
{
    public enum TimerAction {
        Start,
        Tick,
        CountDown
    }
    float time = 0;
    public float Time { get { return time; } }
    [SerializeField] float tickDelaySeconds = 1f;
    UnityEvent OnTimerCountDownEvent;
    UnityEvent OnTimerTickEvent;
    UnityEvent OnTimerStartEvent;
    bool isTicking = false;
    
    private void Awake() {
        if (OnTimerCountDownEvent == null) OnTimerCountDownEvent = new UnityEvent();
        if (OnTimerTickEvent == null) OnTimerTickEvent = new UnityEvent();
        if (OnTimerStartEvent == null) OnTimerStartEvent = new UnityEvent();
    }
    
    public void AddTimerListener(UnityAction listener, TimerAction action){
        UnityEvent selectedEvent = SelectEvent(action);
        if(selectedEvent==null) { Debug.LogError("Event is not initialized!"); return; }
        selectedEvent.AddListener(listener);
    }
    public void RemoveTimerListener(UnityAction listener, TimerAction action){
        
        UnityEvent selectedEvent = SelectEvent(action);
        if(selectedEvent==null) { Debug.LogError("Event is not initialized!"); return; }
        selectedEvent.RemoveListener(listener);
    }
    public void RemoveAllTimerListener(){
        OnTimerCountDownEvent.RemoveAllListeners();
    }
    private UnityEvent SelectEvent(TimerAction action){
        switch (action) {
            case TimerAction.Start:{
                return OnTimerStartEvent;
            }
            case TimerAction.CountDown:{
                return OnTimerCountDownEvent;
            }
            case TimerAction.Tick:{
                return OnTimerTickEvent;
            }
            default: {
                Debug.LogError("Wrong Timer Action Expression");
                return null;
            }
        }
    }
    public void StartTimer(float time){
        if(time <= 0) Debug.LogError("Timer time is not set or less/equals zero!");
        else {
            this.time = time;
            isTicking = true;
            tickingRoutine = StartCoroutine(Tick(tickDelaySeconds));
        }
    }
    public void Pause(){
        isTicking = false;
        StopCoroutine(tickingRoutine);
    }
    public void Resume(){
        StartTimer(time);
    }
    private Coroutine tickingRoutine;
    IEnumerator Tick(float tickDelaySeconds){
        OnTimerStart();
        while(isTicking)
        {
            time -= tickDelaySeconds;
            OnTimerTick();
            if(time<=0){
                isTicking = false;
                OnTimerCountDown();
            }
            yield return new WaitForSeconds(tickDelaySeconds);
        }
    }
    void OnTimerCountDown(){
        OnTimerCountDownEvent.Invoke();
    }
    void OnTimerTick(){
        OnTimerTickEvent.Invoke();
    }
    void OnTimerStart(){
        OnTimerStartEvent.Invoke();
    }
}
