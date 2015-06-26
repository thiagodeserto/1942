using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Destroy", 1.0f);
	}

    void Destroy()
    {
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
