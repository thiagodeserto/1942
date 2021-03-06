﻿using UnityEngine;
using System.Collections;

public class HighscoreManager : Singleton<HighscoreManager> {

    private int highscore;

    [SerializeField]
    private int defaultHighscore;

	void Start () {
        this.highscore = PlayerPrefs.GetInt("highscore", defaultHighscore);
        UIManager.Instance.UpdateHighscore(this.highscore);
	}

    int GetHighscore()
    {
        return this.highscore;
    }

    public void SetScore(int score)
    {
        if(score > this.highscore)
        {
            this.highscore = score;
            PlayerPrefs.SetInt("highscore",score);
            UIManager.Instance.UpdateHighscore(this.highscore);
        }
    }
}
