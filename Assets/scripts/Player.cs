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
    private int maxLives;

    [SerializeField]
    private int lives;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Collider2D collider2d;

    private bool powerup = false;

    private bool dead = false;
    public bool Dead
    {
        get { return this.dead; }
    }

    private int score = 0;
    public int Score
    {
        get { return this.score; }
    }
    
	void Start () {
        UIManager.Instance.UpdateLives(lives, maxLives);
        UIManager.Instance.UpdateScore(0);
	}

    public void AddScore(int score)
    {
        this.score += score;

        UIManager.Instance.UpdateScore(this.score);
    }

    IEnumerator RestartPlayer()
    {
        yield return new WaitForSeconds(3.0f);
        transform.localPosition = Vector3.zero;
        collider2d.enabled = true;
        this.dead = false;
        animator.SetBool("dead", this.dead);
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3.0f);
        UIManager.Instance.GameOver();
    }

    void RemoveLife()
    {
        this.powerup = false;
        this.lives--;
        UIManager.Instance.UpdateLives(lives, maxLives);
        this.dead = true;
        animator.SetBool("dead", this.dead);
        collider2d.enabled = false;

        if (this.lives >= 0)
        {
            StartCoroutine(RestartPlayer());
        }
        else
        {
            StartCoroutine(GameOver());
        }
    }

    public void Fire()
    {
        Vector3 position = new Vector3(transform.position.x, sprite.bounds.max.y, transform.position.z);
        Instantiate((powerup ? this.fire3.gameObject : fire2.gameObject), position, Quaternion.identity);
    }

    private void CollidedWithSomething()
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
            case "Bullet":
                CollidedWithSomething();
                Destroy(collision.collider.gameObject);
                break;
            case "Enemy":
                CollidedWithSomething();
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
