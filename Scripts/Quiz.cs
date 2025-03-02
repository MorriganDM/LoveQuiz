using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; //namespace para TMPro

public class Quiz : MonoBehaviour
{
    [Header("Questions")] //header adiciona um cabeçalho para organizar o inspector.
    [SerializeField] TextMeshProUGUI questionText; //para UI
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true; 

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Accuracy")]
    [SerializeField] TextMeshProUGUI accuracyText;
     AccuracyKeeper accuracyKeeper;
    
    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    void Awake() //awake é melhor para quando se usa FindObject do que Start, pois é inicializado antes do mesmo
    {
        timer = FindAnyObjectByType<Timer>();
        accuracyKeeper = FindAnyObjectByType<AccuracyKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {

            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        accuracyText.text = "Precisão: " + accuracyKeeper.CalculateAccuracy() + "%";
    }

    void DisplayAnswer(int index)
    {
                Image buttonImage;

        if(index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Acertou!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            accuracyKeeper.IncrementCorrectAnswers();
        }
        else
        {
            string correctAnswer = currentQuestion.GetAnswer(currentQuestion.GetCorrectAnswerIndex());
            questionText.text = "A resposta certa era: \n" + correctAnswer;
            buttonImage = answerButtons[currentQuestion.GetCorrectAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        } 
    }

    void GetNextQuestion()
    {
        
        
        if(questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            accuracyKeeper.IncrementQuestionsSeen();
        }
        
        
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
        questions.Remove(currentQuestion);
        }        
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        
        for (int i = 0; i<answerButtons.Length; i++) 
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
            
        }
    }

    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        Image buttonImage;

        for(int i = 0; i < answerButtons.Length; i++)
        {
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

}
