using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;

    void OnEnable()
    {
        GameController.OnKillGremlin += AddScore; // <-- static, no instance ref
    }

    void OnDisable()
    {
        GameController.OnKillGremlin -= AddScore;
    }

    void AddScore()
    {
        score += 10;
        if (scoreText) scoreText.text = score.ToString();
    }
}
