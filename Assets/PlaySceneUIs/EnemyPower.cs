using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPower : MonoBehaviour
{
    public Slider slider;
    private Text textSliderValue;
    // Start is called before the first frame update
    void Start()
    {
        textSliderValue = GetComponent<Text>();
        ShowSliderValue();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSliderValue () {
        string sliderMessage = "(Scroll to change) Enemy Power: " + (int)slider.value;
        textSliderValue.text = sliderMessage;
    }
}
