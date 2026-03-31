using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time;
    public TextMeshProUGUI timerText;
    private bool isRunning;

    private void Start()
    {
        time = 0f;
        isRunning = true;
    }
    void Update()
    {
        if (!isRunning) return;
        time += Time.deltaTime;

        int minutes = (int)(time / 60);
        int Seconds = (int)(time % 60);
        int tenths = (int)((time * 10) % 10);  

        timerText.text = string.Format("{0:00}:{1:00}.{2}", minutes,Seconds, tenths);
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}