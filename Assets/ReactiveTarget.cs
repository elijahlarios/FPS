using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    public int Health = 200;
    public enum HitDirection {
        Forward,
        Backward
    }
    private Animator animator;
    void Start() {
        animator = GetComponent<Animator>();
    }
    public void ReactToHit(HitDirection hitDirection, int Damage) {
        Health -= Damage;
        // Disable the NavMeshAgent component
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            
        }

        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null) {
            
        }   
        if(Health < 0) 
        { 
            StartCoroutine(Die(hitDirection));
            agent.enabled = false;
            behavior.SetAlive(false);
        }
        
    }

    public IEnumerator Die(HitDirection hitDirection) {

        if (hitDirection == HitDirection.Forward)
        {
            animator.SetTrigger("Z_FallingForward");
        }
        else if (hitDirection == HitDirection.Backward)
        {
            animator.SetTrigger("Z_FallingBack");
        }

        // this.transform.Rotate(-75, 0, 0);

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }
}
