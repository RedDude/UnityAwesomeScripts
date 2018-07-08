using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerLabel;

    private float startTime;

    private float time;

    public bool show = false;
    public bool isPaused = false;

    void Awake() {
        startTime = Time.time;
    }

    void FixedUpdate()
    {

        if (isPaused)
            return;

        var time = Time.time - startTime;

        var minutes = Mathf.Floor(time / 60);
        var seconds = Mathf.Floor(time % 60);

        if (show)
        {
            timerLabel.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }
        else {
            timerLabel.text = string.Format("");
        }
        
    }
}
