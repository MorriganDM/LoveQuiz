using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //namespace para TMPro

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText; //para UI
    [SerializeField] QuestionSO question;

    void Start()
    {
        questionText.text = question.GetQuestion();
    }

}
