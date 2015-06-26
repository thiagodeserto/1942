using UnityEngine;
using System.Collections;

public class Player : Singleton<Player> {

    [SerializeField]
    private int maxLives = 3;

    private int lives;

    private int score = 0;
    
	// Use this for initialization
	void Start () {
        this.lives = maxLives;
        UIManager.Instance.UpdateLives(lives, maxLives);
        UIManager.Instance.UpdateScore(0);
	}

    public void AddScore(int score)
    {
        this.score += score;
        UIManager.Instance.UpdateScore(this.score);
    }

    void RemoveLife()
    {
        if(this.lives > 0)
        {
            this.lives--;
            UIManager.Instance.UpdateLives(lives, maxLives);
        }
        else
        {
            UIManager.Instance.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.Kill();
            RemoveLife();
        }
    }
}
