using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerControl : MonoBehaviour
{
    public GameController gameController;
    public Text timerText;
    public float maxTime = 80.0f;
    [SerializeField]
    private float timeRemaining;
    bool isRunning = true;
    int seconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining>0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else if (timeRemaining < 1)
            {
                //announce time is over.
                Debug.Log("Time has run out");
                gameController.SetGameOver();
                isRunning = false;
                Time.timeScale = 0;
            }
            
            seconds = Mathf.FloorToInt(timeRemaining);
            timerText.text = string.Format("{0}", seconds);

        }
    }
}
