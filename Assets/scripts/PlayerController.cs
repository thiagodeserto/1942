using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private float fireFrequency = 0.2f;

    private float lastTimeShot = 0.0f;

    [SerializeField]
    private GameObject fire;

	// Use this for initialization
	void Start () {
	    
	}

    void Fire()
    {
        Instantiate(fire, transform.position, Quaternion.identity);
    }
	
	// Update is called once per frame
    void Update()
    {
        Vector3 translate = Vector3.zero;

        if (Input.GetKey("left"))
        {
            translate += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey("right"))
        {
            translate += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey("up"))
        {
            translate += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey("down"))
        {
            translate += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown("p"))
        {
            bool enabled = Time.timeScale == 1;
            Time.timeScale = (enabled ? 0 : 1);
            UIManager.Instance.SetPaused(enabled);
        }

        if(Input.GetKey("space"))
        {
            if (lastTimeShot + fireFrequency < Time.timeSinceLevelLoad)
            {
                lastTimeShot = Time.timeSinceLevelLoad;
                Fire();
            }
        }

        transform.Translate(translate);
    }
}
