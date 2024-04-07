using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text timelabel;
    int minutes;
    int seconds;
    public float timeCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        timelabel.text = "00:00";   
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        minutes = (int)timeCount/60;
        seconds = (int)timeCount%60;
        timelabel.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
