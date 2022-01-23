using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public AudioClip buttonSound;
    Canvas exit_canvas;
    Canvas rule_canvas;
    private AudioSource audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        exit_canvas = GameObject.Find("ExitCanvas").GetComponent<Canvas>();
        exit_canvas.enabled = false;
        rule_canvas = GameObject.Find("RuleCanvas").GetComponent<Canvas>();
        rule_canvas.enabled = false;
        audioPlayer = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayScene");
    }

    public void OpenRuleCanvas()
    {
        rule_canvas.enabled = true;
    }

    public void CloseRuleCanvas()
    {
        rule_canvas.enabled = false;
    }

    public void OpenExitCanvas()
    {
        exit_canvas.enabled = true;
    }

    public void CloseExitCanvas()
    {
        exit_canvas.enabled = false;
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void PlaySound() {
        PlaySE(buttonSound);
    }

    public void printmessage() {
        print("clicked button!");
    }

    void PlaySE(AudioClip se) {
        audioPlayer.PlayOneShot(se);
    }
}
