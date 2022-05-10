using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Author: Matthew Cruz
// Purpose: Component for Attacking player
public class EnemyAttack : MonoBehaviour
{
    public int damage = -1;
    public GameObject player;
    private PlayerHealth pHealth;
    Animator animator;
    private void Start()
    {
        player = GameObject.Find("Player");
        pHealth = player.GetComponent<PlayerHealth>();
        animator = this.gameObject.GetComponent<Animator>();
    }
    //When a GameObject collides with another GameObject, Unity calls OnCollisionEnter
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("collision is firing!!!!");
            pHealth.updateCurrHealth(damage);
            animator.SetBool("isAttack", true);
        }
        else
        {
            animator.SetBool("isAttack", false);
        }
    }
    
}