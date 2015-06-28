using UnityEngine;
using System.Collections;

public enum EEnemyType
{
    None = 0,
    Enemy1 = 1,
    Enemy2 = 2
}

public class Enemy : MonoBehaviour {

    [SerializeField]
    private EEnemyType enemyType;

    [SerializeField]
    protected Animator animator;

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private float health;

    public void ReceiveDamage(float damage, Vector3 position)
    {
        // Put the explosion always on top of the enemy
        position.z = transform.position.z - 1;
        GameObject explosion = (GameObject)Instantiate(explosionPrefab, position, Quaternion.identity);

        this.health -= damage;
        if(this.health <= 0)
        {
            Player.Instance.AddScore(100);
            if (Player.Instance.Score % 500 == 0)
            {
                ItemManager.Instance.CreateItem(transform.position);
            }
            Destroy(gameObject);
        }
        else
        {
            // Only set the parent if the enemy is not dead - To match with old 1942 style
            explosion.transform.SetParent(transform);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Fire fire = collision.collider.gameObject.GetComponent<Fire>();

        if(fire != null)
        {
            ReceiveDamage(fire.Power, collision.contacts[0].point);
            Destroy(collision.collider.gameObject);
        }
    }
}
