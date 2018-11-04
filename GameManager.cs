using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int level;

    public int spawnPortion = 1;
    public float agentSpeed;
    public float score = 0;
    public int scoreNextLevel;
    
    public Text scoreText;

    int maxSpawnPortion = 4,
        maxLevel = 10,
        maxAgentSpeed = 30;
    

    bool levelMax = false;
    public bool gameOver = false;

    private void Start()
    {
        gameOver = false;
        scoreNextLevel = 15; // main leveling

        level = 1;
        agentSpeed = 20;
    }

    private void Update()
    {
        if (!gameOver)
        {
            score += Time.deltaTime * level;

            ValidScore();
            scoreText.text = "Score: " + (int)score;
        }
        
    }

    void ValidScore()
    {
        if (!levelMax)
        {
            ValidMaxValue();
            if ((int)score >= scoreNextLevel)
            {
                LevelUp();
            }
        }
    }

    void ValidMaxValue()
    {
        spawnPortion = Mathf.Clamp(spawnPortion, 1, maxSpawnPortion);
        level = Mathf.Clamp(level, 1, maxLevel);

        if(level >= maxLevel)
        {
            levelMax = true;
        }
    }

    void LevelUp()
    {   
        level += 1;
        scoreNextLevel += 20;
        spawnPortion += 2;
        agentSpeed += 2;

        agentSpeed = Mathf.Clamp(agentSpeed, 20, maxAgentSpeed);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
