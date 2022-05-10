using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Author: Matthew Cruz
// Purpose: Controlling Enemy Movement and Behavior
public enum EnemyState
{
    Aggro, //Attack and follows player when within a certain radius
    Idle, //Walks to a specified coordinate 
};

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public EnemyState currentState = EnemyState.Aggro;
    public int speed = 1;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = this.gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case(EnemyState.Aggro):
                Aggro();
                break;
            case (EnemyState.Idle):
                Idle();
                break;
        }
    }
    void Aggro()
    {
        animator.SetBool("isMoving", true);
        transform.LookAt(player.transform.position);
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 5f * Time.deltaTime);
    }
    void Idle()
    {
        animator.SetBool("isMoving", false);
    }
}
