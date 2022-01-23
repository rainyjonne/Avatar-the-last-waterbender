using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar health_bar;
    public float speed = 2.0f;
    public GameObject destination;
    public GameObject blood_prefab;
    public GameObject player;
    public AudioClip enemyBallCreateSE;
    public NavMeshAgent m_naviAgent;
    public int frame_threshold = 1;
    [SerializeField] Transform _CreationPoint;
    GameObject now_blood;
    public EnemyBall enemy_ball_prefab;
    EnemyBall enemy_ball;
    private AudioSource audioPlayer;
    bool is100FramesAdded;
    int count = 0;
    private Animator m_animator;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        health_bar.SetMaxHealth(maxHealth);
        m_animator = gameObject.GetComponent<Animator>();
        now_blood = GameObject.Find("BloodBurst");
        audioPlayer = gameObject.GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        // frame count
        count = count + 1;

        // distance
        var dist = Vector3.Distance(destination.transform.position, gameObject.transform.position);
        if (m_naviAgent.stoppingDistance < dist && dist < 10) {
            m_animator.SetFloat("Speed", speed);
            if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Dying"))
            {
                m_naviAgent.SetDestination(gameObject.transform.position);
            } else {
                m_naviAgent.SetDestination(destination.transform.position);
            }
        }

        if (dist <= m_naviAgent.stoppingDistance) {
            FaceTarget();
            m_animator.SetFloat("Speed", 0);


            if (!EnemyBallCreated())
            {
                // PrepareAttack
                //m_animator.SetBool("PrepareAttack", true);
                //m_animator.SetBool("Attack", false);
                CreateEnemyBall();
                PlaySE(enemyBallCreateSE);
            }
            else
            {
                if(count % frame_threshold == 0) {
                    // Attack
                    //m_animator.SetBool("PrepareAttack", false);
                    m_animator.SetBool("Attack", true);

                    if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        print("wtf");
                        if (enemy_ball != null)
                        {
                            ThrowEnemyBall(player.transform.position);
                        }
                        m_animator.SetBool("Attack", false);

                    }
                }
            }
        }
        
    }

    void OnCollisionEnter(Collision other) {
 
        if (other.gameObject.name.Contains("WaterBall")) {
            // health bar go down
            // default for 20
            TakeDamage(20);

            // blood effect
            if (now_blood == null) {
                now_blood = Instantiate(blood_prefab, gameObject.transform.position, Quaternion.identity);
                //blood_particle_sys.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
            } 
            now_blood.gameObject.GetComponent<ParticleSystem>().Play(true);
        }
        //blood_particle_sys.Play(true);
    }

    void OnCollisionExit(Collision other){
        if (now_blood) {
            Destroy(now_blood, 1.0f);
        }
    }

    public bool EnemyBallCreated()
    {
        return enemy_ball != null;
    }
    public void CreateEnemyBall()
    {
        enemy_ball = Instantiate(enemy_ball_prefab, _CreationPoint.position, Quaternion.identity);
    }

    public void ThrowEnemyBall(Vector3 pos)
    {
        enemy_ball.Throw(pos);
    }

    void FaceTarget ()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    
    void PlaySE(AudioClip se) {
        // Play water sound effect
        audioPlayer.PlayOneShot(se);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        health_bar.SetHealth(currentHealth);
    }


}
