using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSEController : MonoBehaviour
{
    public AudioClip buttonSound;
    public AudioClip slideSound;
    private AudioSource audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = gameObject.GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void PlaySE(AudioClip se) {
        audioPlayer.PlayOneShot(se);
    }

    public void PlaySound() {
        PlaySE(buttonSound);
    }

    public void PlaySlideSound() {
        PlaySE(slideSound);
    }
}
