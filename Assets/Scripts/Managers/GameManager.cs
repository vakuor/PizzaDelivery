using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton Init  
    public static GameManager instance = null;
    private void Awake() {
        if (instance == null) {
	    instance = this;
	} else if(instance != this){
	    Destroy(gameObject);
        return;
	}
	    //DontDestroyOnLoad(gameObject);
	    InitializeManager();
    }
    private void InitializeManager(){ 
        /* TODO: */
     }

    #endregion

    [SerializeField] private float deathDelay = 0f;
    public float DeathDelay { get { return deathDelay; } }
    private List<EnemyBehaviour> enemies = new List<EnemyBehaviour>();
    public List<EnemyBehaviour> Enemies { get { return enemies; } }

    [SerializeField] GameObject enemiesContainer;
    [SerializeField] GameObject playerGO;
    public GameObject PlayerGO { get { return playerGO; } }

    public void RegisterEnemy(EnemyBehaviour beh){
        enemies.Add(beh);
    }
    public void UnregisterEnemy(EnemyBehaviour enemy){
        enemies.Remove(enemy);
    }
    //[SerializeField] Timer gameFlowTimer; // TODO:

    public void OnTowerReached(bool inside){
        Debug.Log("OnTowerReached: " + inside);
    }

    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private TextMeshProUGUI targetsUI;
    private void Start() {
        InitGameStart();
    }
    [SerializeField] List<AudioClip> deathClips;
    [SerializeField] List<AudioClip> shootClips;
    public AudioClip GetRandomAudioClip(string category){
        switch(category){
            case "Death":{
            int n = Random.Range(0, deathClips.Count-1);
            return deathClips[n];
            }
            case "Shoot":{
                
            int n = Random.Range(0, shootClips.Count-1);
            return shootClips[n];
            }
            default:{
                Debug.LogError("RandomAudioClipCategory is not found!");
                return null;
            }
        }
    }
    public void CloseGame(){
        Application.Quit();
    }

    #region GameFlow
    [SerializeField] private float gameTime = 140f;
    [SerializeField] private Timer gameFlowTimer;
    [SerializeField] private GameObject gameOverUIGO;
    [SerializeField] private GameObject gameFlowUIGO;
    [SerializeField] private GameObject gameWinUIGO;
    public UnityEvent OnTowerReachedEvent;
    [SerializeField] private bool isGameRunning = false;
    private int currentTargets;
    private int maxTargets;
    public void OnTargetHit(){
        currentTargets--;
        targetsUI.SetText(currentTargets.ToString()+"/"+maxTargets);
        if(currentTargets<=0){
            GameWin();
        }
    }
    public bool IsGameRunning { get { return isGameRunning; } }
    public void RestartLevel(){
        Debug.Log("Restart!");
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }
    void InitGameStart(){
        if(gameFlowTimer==null) { Debug.LogError("GameFlowTimer is not set!"); return; }
        maxTargets = enemiesContainer.transform.childCount;
        currentTargets = maxTargets;
        OnTowerReachedEvent = new UnityEvent();
        targetsUI.SetText(currentTargets.ToString()+"/"+maxTargets);
        gameFlowTimer.AddTimerListener(StartLog, Timer.TimerAction.Start);
        gameFlowTimer.AddTimerListener(TickLog, Timer.TimerAction.Tick);
        gameFlowTimer.AddTimerListener(GameOver, Timer.TimerAction.CountDown);
        gameFlowTimer.StartTimer(gameTime);
        Time.timeScale = 1f;
        isGameRunning = true;
    }
    private void StartLog(){
        float time = gameFlowTimer.Time;
        tmp.SetText(System.TimeSpan.FromSeconds((int)time).ToString(@"mm\:ss"));
    }
    private void TickLog(){
        float time = gameFlowTimer.Time;
        tmp.SetText(System.TimeSpan.FromSeconds((int)time).ToString(@"mm\:ss"));
    }
    private void GameOver(){
        Debug.Log("Game Over!");
        gameOverUIGO.SetActive(true);
        gameFlowUIGO.SetActive(false);
        Time.timeScale = 0.2f;
        isGameRunning = false;
        gameFlowTimer.Pause();
    }
    private void GameWin(){
        Debug.Log("Game Win!");
        gameWinUIGO.SetActive(true);
        gameFlowUIGO.SetActive(false);
        Time.timeScale = 0.2f;
        isGameRunning = false;
        gameFlowTimer.Pause();
    }
    #endregion
}
