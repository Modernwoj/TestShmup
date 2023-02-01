using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuUI;
    [SerializeField]
    private GameObject gameplayUI;
    [SerializeField]
    private Text highscoreText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private GameObject lifePrefab;
    private List<GameObject> lifes = new List<GameObject>();
    private Vector2 lifeSize;

    private void Start()
    {
        Rect rect = lifePrefab.GetComponent<Image>().sprite.rect;
        lifeSize = new Vector2(rect.width, rect.height);
        Debug.Log(lifeSize);
    }

    public void UpdateTime(int time)
    {
        timeText.text = time.ToString();
    }

    public void UpdateHighscore(int score)
    {
        Debug.Log(score);
        highscoreText.text = score.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetActiveUIGameplay()
    {
        gameplayUI.SetActive(true);
        menuUI.SetActive(false);
    }
    public void SetActiveUIMenu()
    {
        menuUI.SetActive(true);
        gameplayUI.SetActive(false);
        UpdateHighscore(GameManager.instance.highscore);
    }
    public void SetLife(int life)
    {
        for (int i = 0; i < life; i++)
        {
            var newObj = Instantiate(lifePrefab);
            var pos = newObj.transform.position;
            pos.x = lifeSize.x * (i - (life - 1)/2) * lifePrefab.transform.localScale.x;
            newObj.transform.position = pos;
            newObj.transform.SetParent(gameplayUI.transform, false);
            lifes.Add(newObj);
        }
    }

    public void TakeLife()
    {
        var life = lifes[lifes.Count - 1];
        lifes.RemoveAt(lifes.Count - 1);
        Destroy(life);
    }
}