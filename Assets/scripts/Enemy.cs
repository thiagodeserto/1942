using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject explosion;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
	}

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
            UIManager.Instance.AddScore(100);
        }
        
        Kill();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 diff = player.transform.position - transform.position;
        transform.Translate(diff.normalized * Time.deltaTime);
	}
}
