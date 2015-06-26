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

    private bool usingMouse = false;

    private Vector3 lastMousePosition;

	// Use this for initialization
	void Start () {
        this.cam = Camera.main;
        lastMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
	}

    void Fire()
    {
        Instantiate(fire, transform.position, Quaternion.identity);
    }

    private void CheckController(Vector3 mousePosition)
    {
        if (mousePosition != lastMousePosition)
        {
            this.usingMouse = true;
        }
        else if (Input.anyKeyDown)
        {
            this.usingMouse = false;
        }
        lastMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }
	
	// Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        CheckController(mousePosition);

        if(this.usingMouse)
        {
            mousePosition.z = transform.position.z;
            Vector3 direction = Vector3.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
            if (direction.x < -0.8f || direction.x > 0.8f)
            {
                direction.x = transform.position.x;
            }
            if (direction.y < -1.35f || direction.y > 1.35f)
            {
                direction.y = transform.position.y;
            }
            transform.position = direction;
        }
        else
        {
            Vector3 translate = Vector3.zero;
            if (Input.GetKey("left") && transform.position.x > -0.8f)
            {
                translate += Vector3.left * speed * Time.deltaTime;
            }
            if (Input.GetKey("right") && transform.position.x < 0.8f)
            {
                translate += Vector3.right * speed * Time.deltaTime;
            }
            if (Input.GetKey("up") && transform.position.y < 1.35f)
            {
                translate += Vector3.up * speed * Time.deltaTime;
            }
            if (Input.GetKey("down") && transform.position.y > -1.35f)
            {
                translate += Vector3.down * speed * Time.deltaTime;
            }

            transform.Translate(translate);
        }

        if(Input.GetKey("space") || Input.GetMouseButton(0))
        {
            if (lastTimeShot + fireFrequency < Time.timeSinceLevelLoad)
            {
                lastTimeShot = Time.timeSinceLevelLoad;
                Fire();
            }
        }
    }
}
