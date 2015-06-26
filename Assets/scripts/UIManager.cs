using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager> {

    [SerializeField]
    private Text highscoreText;

    [SerializeField]
    private GameObject lifePrefab;

    private List<GameObject> livesObjects = new List<GameObject>();

    [SerializeField]
    private Transform lowerRight;

    [SerializeField]
    private GameObject pause;

    [SerializeField]
    private GameObject gameover;

    private int highscore = 0;
    public void AddScore(int score)
    {
        this.highscore += score;
        highscoreText.text = highscore.ToString();
    }

	// Use this for initialization
	void Start () {
        
	}

    public void TogglePause()
    {
        bool enabled = Time.timeScale == 1;
        Time.timeScale = (enabled ? 0 : 1);
        pause.SetActive(enabled);
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
