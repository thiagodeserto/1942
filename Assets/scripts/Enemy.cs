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

    void OnTriggerEnter2D(Collider2D collider)
    {
        UIManager.Instance.AddScore(100);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(collider.gameObject);
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 diff = player.transform.position - transform.position;
        transform.Translate(diff.normalized * Time.deltaTime);
	}
}
