using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI finalScoreText;
    AccuracyKeeper accuracyKeeper;
    void Awake()
    {
        accuracyKeeper = FindAnyObjectByType<AccuracyKeeper>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "" + accuracyKeeper.CalculateAccuracy() + "%";
    }
}
