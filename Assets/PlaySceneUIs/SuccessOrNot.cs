using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class SuccessOrNot : MonoBehaviour
{
    public Slider enemy1_healthBar;
    public Slider enemy2_healthBar;
    public Animator enemy1_animator;
    public Animator enemy2_animator;
    public Slider enemy3_healthBar;
    public Animator enemy3_animator;
    public NavMeshAgent enemy1_nav;
    public NavMeshAgent enemy2_nav;
    public NavMeshAgent enemy3_nav;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    Canvas success_canvas;
    Canvas main_canvas;


    // Start is called before the first frame update
    void Start()
    {
        success_canvas = GameObject.Find("SuccessCanvas").GetComponent<Canvas>();
        success_canvas.enabled = false;
        main_canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy1_healthBar.value == 0) {
            enemy1_animator.SetBool("Dead", true);
            enemy1_nav.SetDestination(enemy1.transform.position);
        }

        if (enemy2_healthBar.value == 0) {
            enemy2_animator.SetBool("Dead", true);
            enemy2_nav.SetDestination(enemy1.transform.position);
        }

        if (enemy3_healthBar.value == 0) {
            enemy3_animator.SetBool("Dead", true);
            enemy3_nav.SetDestination(enemy1.transform.position);
        }


        if (enemy1_healthBar.value == 0 && enemy2_healthBar.value == 0 && enemy3_healthBar.value == 0) {
            success_canvas.enabled = true;
            main_canvas.enabled = false;
        }
        
    }
}
