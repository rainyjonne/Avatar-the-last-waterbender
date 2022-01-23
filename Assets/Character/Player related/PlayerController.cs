using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    public float forwardSpeed = 2.0f;
    public float rotateSpeed = 2.0f;
    public GameObject player_blood_prefab;
    private Animator m_animator;
    Vector3 initPos;
    float attackPower = 20.0f;

    [SerializeField] bool _update;
    [SerializeField] Transform _CreationPoint;
    [SerializeField] WaterBall WaterBallPrefab;
    WaterBall waterBall;
    GameObject now_player_blood;
    public AudioClip watersmashSE;
    public AudioClip waterballSE;
    private AudioSource audioPlayer;
    Canvas defeated_canvas;

    // Start is called before the first frame update
    void Start()
    {
        defeated_canvas = GameObject.Find("DefeatedCanvas").GetComponent<Canvas>();
        defeated_canvas.enabled = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        initPos = transform.position;
        m_animator = gameObject.GetComponent<Animator>();
        now_player_blood = GameObject.Find("PlayerBlood");
        audioPlayer = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0 ){
            defeated_canvas.enabled = true;
        }
        
        m_animator.SetBool("Attack", false);
        m_animator.SetBool("PrepareAttack", false);



        // Attack
        if (Input.GetMouseButtonDown(1))
        {
            if (!WaterBallCreated())
            {
                // PrepareAttack
                m_animator.SetBool("PrepareAttack", true);
                m_animator.SetBool("Attack", false);
                CreateWaterBall();
                PlaySE(waterballSE);
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // Attack
                    m_animator.SetBool("Attack", true);
                    m_animator.SetBool("PrepareAttack", false);
                    if (waterBall != null)
                    {
                        ThrowWaterBall(hit.point);
                    }
                    PlaySE(watersmashSE);
                }
            }
        }

        // Back to initial position
        if (transform.position.y < -0.3f)
        {
            transform.position = initPos;
        }
        
    }

    void FixedUpdate()
    {
        // Get Input
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        m_animator.SetFloat("Speed", v);


        Vector3 velocity = new Vector3(0.0f, 0.0f, v);

        // Transform
        gameObject.transform.Rotate(0, h*rotateSpeed*0.8f, 0); 
        gameObject.transform.Translate(velocity*forwardSpeed*Time.fixedDeltaTime);

    }


    void OnCollisionEnter(Collision other) {
 
        if (other.gameObject.name.Contains("EnemyBall")) {
            // health bar go down
            TakeDamage((int)attackPower);

            // blood effects
            if (now_player_blood == null) {
                now_player_blood = Instantiate(player_blood_prefab, gameObject.transform.position, Quaternion.identity);
                //blood_particle_sys.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
            } 
            now_player_blood.gameObject.GetComponent<ParticleSystem>().Play(true);
        }
        //blood_particle_sys.Play(true);
    }

    void OnCollisionExit(Collision other){
        if (now_player_blood) {
            Destroy(now_player_blood, 1.0f);
        }
    }

    public bool WaterBallCreated()
    {
        return waterBall != null;
    }
    public void CreateWaterBall()
    {
        waterBall = Instantiate(WaterBallPrefab, _CreationPoint.position, Quaternion.identity);
    }

    public void ThrowWaterBall(Vector3 pos)
    {
        waterBall.Throw(pos);
    }


    void PlaySE(AudioClip se) {
        // Play water sound effect
        audioPlayer.PlayOneShot(se);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void SetAttackPower(float power){
        attackPower = power;
    }

    public void ChangeScene(int idx) {
        if (idx == 1){
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MenuScene");
        }
        if (idx == 2){
            Application.Quit();
        }
    }

    public void LeaveGame(){
        Application.Quit();
    }

    public void GoToMenu(){
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MenuScene");
    }

}
