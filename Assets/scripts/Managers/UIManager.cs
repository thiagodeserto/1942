using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager> {

    [SerializeField]
    private Text highscoreText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private GameObject lifePrefab;

    private List<GameObject> livesObjects = new List<GameObject>();

    [SerializeField]
    private Transform lowerRight;

    [SerializeField]
    private GameObject pause;

    [SerializeField]
    private GameObject gameover;

    private bool paused = false;
    public bool Paused
    {
        get { return this.paused; }
    }

    public void TogglePause()
    {
        paused = !paused;
        Time.timeScale = (paused ? 0 : 1);
        pause.SetActive(paused);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameover.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void UpdateScore(int score)
    {
        this.scoreText.text = score.ToString();
        HighscoreManager.Instance.SetScore(score);
    }

    public void UpdateHighscore(int highscore)
    {
        this.highscoreText.text = highscore.ToString();
    }

    public void UpdateLives(int lives, int maxLives)
    {
        while(livesObjects.Count < maxLives)
        {
            Vector3 position = lowerRight.transform.position;
            position -= new Vector3(25+ (50 * livesObjects.Count),-25,0);
            GameObject lifeGO = Instantiate(lifePrefab, position, Quaternion.identity) as GameObject;
            lifeGO.transform.SetParent(lowerRight.transform);
            livesObjects.Add(lifeGO);
        }
        for(int i = 0; i < livesObjects.Count; i++)
        {
            livesObjects[i].SetActive(lives > i);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("p"))
        {
            TogglePause();
        }
	}
}
