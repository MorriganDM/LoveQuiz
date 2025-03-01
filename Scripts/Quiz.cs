using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; //namespace para TMPro

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText; //para UI
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] int correctAnswerIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    void Start()
    {
        DisplayQuestion();
    }

    public void OnAnswerSelected(int index)
    {

        Image buttonImage;

        if(index == question.GetCorrectAnswerIndex())
        {
            questionText.text = "Acertou!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            string correctAnswer = question.GetAnswer(question.GetCorrectAnswerIndex());
            questionText.text = "A resposta certa era: \n" + correctAnswer;
            buttonImage = answerButtons[question.GetCorrectAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        } 

        SetButtonState(false);
    }

    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        questionText.text = question.GetQuestion();

        
        for (int i = 0; i<answerButtons.Length; i++) 
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
            
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
