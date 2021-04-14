using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEnableDisable : MonoBehaviour
{
    [SerializeField] private bool initialIsRagdoll = false;
    private List<Rigidbody> ragdollBones = new List<Rigidbody>();
    private List<Transform> bodyParts = new List<Transform>();
    [SerializeField] private Animator animator;
    
    void Start()
    {
        FindAndAddChildren(transform);
        FindAndAddBones();

        if(initialIsRagdoll)
            SetRagdoll(false);
        if(animator==null) Debug.LogWarning("Animator is null!");
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            SwitchPhysics();
        }
    }

    void FindAndAddChildren(Transform parent){
        if(parent.childCount>0){
            for(int i=0;i<parent.childCount;i++){
                Transform child = parent.GetChild(i);
                bodyParts.Add(child);
                FindAndAddChildren(child);
            }
        }
    }

    void FindAndAddBones(){
        Rigidbody rg;
        foreach(Transform t in bodyParts){
            rg = t.GetComponent<Rigidbody>();
            if(rg) ragdollBones.Add(rg);
        }
    }

    public void SetRagdoll(bool enabled){
        if(animator!=null){
            animator.enabled = !enabled;
        }
        foreach(Rigidbody rb in ragdollBones){
            rb.isKinematic = !enabled;
        }
    }

    public void SwitchPhysics(){
        if(animator!=null){
            animator.enabled = !animator.enabled;
        }
        foreach(Rigidbody rb in ragdollBones){
            rb.isKinematic = !rb.isKinematic;
        }
    }
}
