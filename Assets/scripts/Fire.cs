using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

    [SerializeField]
    private float speed = 3.0f;

	// Use this for initialization
	void Start () {
        Invoke("AutoDestroy", 1.0f);
	}

    void AutoDestroy()
    {
        // TODO: change to  pool release
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
	}
}
