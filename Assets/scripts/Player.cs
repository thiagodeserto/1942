using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField]
    private int maxLives = 3;

    private int lives;
    
	// Use this for initialization
	void Start () {
        this.lives = maxLives;
        UIManager.Instance.UpdateLives(lives, maxLives);
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
	
	// Update is called once per frame
	void Update () {
	
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
