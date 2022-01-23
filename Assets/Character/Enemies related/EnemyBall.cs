using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    public ParticleSystem _EnemyBallParticleSystem;
    [SerializeField] AnimationCurve _SpeedCurve;
    [SerializeField] float _Speed;
    public GameObject _SmashPrefab;
    public AudioClip smashSE;
    public AudioClip hurt_SE;
    private AudioSource audioPlayer;
    GameObject now_smash;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = gameObject.GetComponent<AudioSource>();
        now_smash = GameObject.Find("FireSmash");
        rb = gameObject.GetComponent<Rigidbody>();

    }

    public void Throw(Vector3 target)
    {
        StopAllCoroutines();
        StartCoroutine(Coroutine_Throw(target));
    }

    IEnumerator Coroutine_Throw(Vector3 target) 
    {
        float lerp = 0;
        Vector3 startPos = transform.position;
        while (lerp < 0)
        {
            transform.position = Vector3.Lerp(startPos, target, _SpeedCurve.Evaluate(lerp));
            float magnitude = (transform.position - target).magnitude;
            if (magnitude < 0.4f)
            {
                break;
            }
            lerp += Time.deltaTime * _Speed;
            yield return null;
        }

        Vector3 forward = target - startPos;
        forward.y = 0;
        rb.MovePosition(target + forward * Time.deltaTime * _Speed);
        //rb.transform.forward = forward;
        //_EnemyBallParticleSystem.transform.forward = forward;
        _EnemyBallParticleSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        
        if (now_smash == null) {
            now_smash = Instantiate(_SmashPrefab, target, Quaternion.identity);
        }
        now_smash.transform.forward = forward;
        now_smash.gameObject.GetComponent<ParticleSystem>().Play(true);
        // if (Vector3.Angle(startPos - target, Vector3.up) > 30)
        // {
        //     ParticleSystem spill = Instantiate(_SpillPrefab, target, Quaternion.identity);
        //     spill.transform.forward = forward;
        // }
        Destroy(gameObject, 2.0f);
        Destroy(now_smash, 1.0f);
        PlaySE(smashSE);
    }
 
     
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.name == "Player") {
            PlaySE(hurt_SE);
        }
     }
        

    void PlaySE(AudioClip se) {
        // Play water sound effect
        audioPlayer.PlayOneShot(se);
    }


}

