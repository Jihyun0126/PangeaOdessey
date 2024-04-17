using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Animator animator;
    public Transform player;
    public float speed;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    public void DirectionEnemy(float target, float baseobj)
    {
        if (target < baseobj)
            animator.SetFloat("Direction",-1);
        else
            animator.SetFloat("Direction",1);
    }
}
