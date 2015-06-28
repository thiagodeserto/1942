using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private float health;

    public void ApplyDamage(float damage)
    {
        this.health -= damage;
        if(this.health <= 0)
        {
            Player.Instance.AddScore(100);
            if (Player.Instance.Score % 500 == 0)
            {
                ItemManager.Instance.CreateItem(transform.position);
            }
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Fire fire = collider.gameObject.GetComponent<Fire>();

        if(fire != null)
        {
            ApplyDamage(fire.Power);
            Destroy(collider.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 diff = Player.Instance.transform.position - transform.position;
        transform.Translate(diff.normalized * this.speed * Time.deltaTime);
	}
}
