using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private float health;

    public void ApplyDamage(float damage, Vector3 position)
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
            ApplyDamage(fire.Power, collision.contacts[0].point);
            Destroy(collision.collider.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 diff = Player.Instance.transform.position - transform.position;
        transform.Translate(diff.normalized * this.speed * Time.deltaTime);
	}
}
