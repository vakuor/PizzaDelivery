using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private bool isAlive = true;
    public bool IsAlive { get { return isAlive; } }
    [SerializeField] private RagdollEnableDisable ragdollEnableDisable;

    [SerializeField] private AudioSource localAudio;
    private GameObject player;
    private bool isRendered = false;
    private List<Transform> children;
    private void Start() {
        GameManager.instance.RegisterEnemy(this);
        player = GameManager.instance.PlayerGO;
        if(transform.childCount > 0){
            children = new List<Transform>();
            for(int i=0;i<transform.childCount;i++){
                children.Add(transform.GetChild(i));
            }
        }
    }
    private void Update() {
        if(isAlive && isRendered){
            FaceTo(player);
        }
        
    }
    public void OnDeath(){
        //Debug.Log("OnDeath!");
        if(!isAlive) return;
        GameManager.instance.UnregisterEnemy(this);
        GameManager.instance.OnTargetHit();
        PlaySound(GameManager.instance.GetRandomAudioClip("Death"));
        isAlive = false;
        ragdollEnableDisable.SetRagdoll(true);
        Destroy(gameObject, GameManager.instance.DeathDelay+localAudio.clip.length);
    }
    private void PlaySound(AudioClip clip){
        localAudio.clip = clip;
        localAudio.Play();
    }
    private void FaceTo(GameObject target){
        Vector3 vec = Vector3.RotateTowards(transform.forward, target.transform.position-transform.position, 7, 0.0f);
        vec.y = 0;
        transform.rotation = Quaternion.LookRotation(vec);
    }
    private void OnBecameVisible() {
        isRendered = true;
        if(children!=null){
            for(int i=0;i<children.Count;i++){
                children[i].gameObject.SetActive(true);
            }
        }
    }
    private void OnBecameInvisible() {
        isRendered = false;
        if(children!=null){
            for(int i=0;i<children.Count;i++){
                children[i].gameObject.SetActive(false);
            }
        }
    }
}
