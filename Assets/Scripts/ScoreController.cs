using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public int GremlinPoints = 1;

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
        score += GremlinPoints;
        if (scoreText) scoreText.text = score.ToString();
    }
}
