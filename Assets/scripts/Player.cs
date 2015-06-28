using UnityEngine;
using System.Collections;

public class Player : Singleton<Player> {

    [SerializeField]
    private Fire fire1;

    [SerializeField]
    private Fire fire2;

    [SerializeField]
    private Fire fire3;

    [SerializeField]
    private int maxLives = 3;

    private int lives;

    private bool powerup = false;

    private int score = 0;
    public int Score
    {
        get { return this.score; }
    }
    
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
            this.powerup = false;
            this.lives--;
            UIManager.Instance.UpdateLives(lives, maxLives);
        }
        else
        {
            UIManager.Instance.GameOver();
        }
    }

    public void Fire()
    {
        Instantiate((powerup ? this.fire3.gameObject : fire2.gameObject), transform.position, Quaternion.identity);
    }

    private void CollidedWithAnEnemy(Enemy enemy)
    {
        enemy.ApplyDamage(10.0f);
        RemoveLife();
    }

    private void CollidedWithAnItem(Item item)
    {
        this.powerup = true;
        ParallaxManager.Instance.RemoveMovable(item);
        Destroy(item.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch(collider.gameObject.tag)
        {
            case "Enemy":
                Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    CollidedWithAnEnemy(enemy);
                }
                break;
            case "Item":
                Item item = collider.gameObject.GetComponent<Item>();
                if(item != null)
                {
                    CollidedWithAnItem(item);
                }
                break;
            default:
                break;
        }
    }
}
