using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject explosion;

    public void Kill()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Fire fire = collider.gameObject.GetComponent<Fire>();

        if(fire != null)
        {
            Destroy(collider.gameObject);
            Player.Instance.AddScore(100);
        }
        
        Kill();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 diff = Player.Instance.transform.position - transform.position;
        transform.Translate(diff.normalized * this.speed * Time.deltaTime);
	}
}
