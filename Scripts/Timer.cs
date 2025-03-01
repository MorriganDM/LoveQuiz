using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    
    public bool isAnsweringQuestion = false;
    
    float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime; //-= subtracts the left value to the right value

        if(isAnsweringQuestion)
        {
            if(timerValue <= 0)
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if(timerValue <= 0)
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
            }
        }

        Debug.Log(timerValue);
    }
}
