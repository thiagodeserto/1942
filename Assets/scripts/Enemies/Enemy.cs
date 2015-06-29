using UnityEngine;
using System.Collections;

public enum EEnemyType
{
    None = 0,
    Simple = 1,
    Big = 2
}

[System.Serializable]
public struct SimpleRange
{
    public float min;
    public float max;
}

public class Enemy : MonoBehaviour {

    [SerializeField]
    private EEnemyType enemyType;
    public EEnemyType EnemyType
    {
        get { return this.enemyType; }
    }

    [SerializeField]
    protected Animator animator;

    [SerializeField]
    private SpriteRenderer sprite;
    public SpriteRenderer Sprite
    {
        get { return this.sprite; }
    }

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float health;

    public void Shoot(float speed, Vector3 direction)
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.FillData(speed, direction);
    }

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
            AutoDestroy();
        }
        else
        {
            // Only set the parent if the enemy is not dead - To match with old 1942 style
            explosion.transform.SetParent(transform);
        }
    }

    public virtual void AutoDestroy()
    {
        Destroy(gameObject);
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
