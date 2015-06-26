using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

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

    private Bounds cameraBounds;

	// Use this for initialization
	void Start () {
        this.cam = Camera.main;

        lastMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        float screenAspect = ((float)Screen.width) / (float)Screen.height;
        float cameraHeight = cam.orthographicSize * 2.0f;
        // TODO: Correct camera bounds with player bounds
        cameraBounds = new Bounds(cam.transform.position, new Vector3(cameraHeight * screenAspect * 0.85f, cameraHeight * 0.94f, 0));
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
            if (direction.x < cameraBounds.min.x || direction.x > cameraBounds.max.x)
            {
                direction.x = transform.position.x;
            }
            if (direction.y < cameraBounds.min.y || direction.y > cameraBounds.max.y)
            {
                direction.y = transform.position.y;
            }
            transform.position = direction;
        }
        else
        {
            Vector3 translate = Vector3.zero;
            if (Input.GetKey("left") && transform.position.x > cameraBounds.min.x)
            {
                translate += Vector3.left * speed * Time.deltaTime;
            }
            if (Input.GetKey("right") && transform.position.x < cameraBounds.max.x)
            {
                translate += Vector3.right * speed * Time.deltaTime;
            }
            if (Input.GetKey("up") && transform.position.y < cameraBounds.max.y)
            {
                translate += Vector3.up * speed * Time.deltaTime;
            }
            if (Input.GetKey("down") && transform.position.y > cameraBounds.min.y)
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
