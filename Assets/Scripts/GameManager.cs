using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public static float yBounds = 4;
    public int highscore = 0;
    public float roundTime = 60f; //5min in seconds as baseline
    public enum gameState
    {
        Invalid = -1,
        Menu,
        Gameplay,
    }
    public gameState currentState = gameState.Invalid;

    private int currentScore = 0;
    private float currentTime = 0f;

    [SerializeField]
    private GUIController gameUI;
    [SerializeField]
    private SpawnerController spawner;
    [SerializeField]
    private PlayerMovement player;
    //[SerializeField]
    private int maxLifes = 3;
    private int lifes;

    void Start()
    {
        if (instance != null)
            Destroy(this);
        else
        {
            instance = this;
            SetGameState(gameState.Menu);
        }
    }

    private void Update()
    {
        if (currentState == gameState.Menu && Input.anyKey)
            SetGameState(gameState.Gameplay);
        else if(currentState == gameState.Gameplay)
        {
            currentTime -= Time.deltaTime;
            gameUI.UpdateTime((int)currentTime);
            if (currentTime < 0) {
                EndRound();
            }
        }
    }


    public void SetGameState(gameState state)
    {
        currentState = state;

        switch (state)
        {
            case gameState.Menu:
                StateMenu();
                break;
            case gameState.Gameplay:
                StateGameplay();
                break;
        }
    }

    public void StateMenu()
    {

        Time.timeScale = 0;
        highscore = PlayerPrefs.GetInt("highscore", 0);

        gameUI.SetActiveUIMenu();
        gameUI.UpdateHighscore(highscore);
        spawner.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
    }

    public void StateGameplay()
    {
        currentTime = roundTime;
        Time.timeScale = 1;
        currentScore = 0;
        lifes = maxLifes;

        DisplayLife();
        gameUI.SetActiveUIGameplay();
        spawner.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
    }

    private void DisplayLife()
    {
        if (lifes == maxLifes)
            gameUI.SetLife(lifes);
        else
            gameUI.TakeLife();
    }

    public void TakeLife()
    {
        lifes--;
        DisplayLife();
        if (lifes == 0)
        {
            EndRound();
        }
    }

    public void IncreaseScore()
    {
        currentScore++;
        gameUI.UpdateScore(currentScore);
    }

    private void EndRound()
    {
        if(highscore < currentScore)
            PlayerPrefs.SetInt("highscore", currentScore);  //save manager for 1 value is kinda overkill
        SetGameState(gameState.Menu);
    }
}
