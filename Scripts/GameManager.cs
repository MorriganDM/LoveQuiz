using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour //gamemanagers são comuns para manipulação de cenas.
{
 
    Quiz quiz;
    EndScreen endScreen;
    
    void Awake() 
    {
        quiz = FindAnyObjectByType<Quiz>();
        endScreen = FindAnyObjectByType<EndScreen>();       
    }

    //IDEALMENTE SE SEPARA MÉTODOS SetActive no start e FindObject em Awake.
    void Start()
    {
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if(quiz.isComplete)
            {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore(); 
            }
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
