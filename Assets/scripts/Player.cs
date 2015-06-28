using UnityEngine;
using System.Collections;

public class Player : Singleton<Player> {

    [SerializeField]
    private SpriteRenderer sprite;

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
        Vector3 position = new Vector3(transform.position.x, sprite.bounds.max.y, transform.position.z);
        Instantiate((powerup ? this.fire3.gameObject : fire2.gameObject), position, Quaternion.identity);
    }

    private void CollidedWithAnEnemy(Enemy enemy)
    {
        RemoveLife();
    }

    private void CollidedWithAnItem(Item item)
    {
        this.powerup = true;
        ParallaxManager.Instance.RemoveMovable(item);
        Destroy(item.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.collider.gameObject.tag)
        {
            case "Enemy":
                Enemy enemy = collision.collider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    CollidedWithAnEnemy(enemy);
                }
                break;
            case "Item":
                Item item = collision.collider.gameObject.GetComponent<Item>();
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
