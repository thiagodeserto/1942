using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private float fireFrequency = 0.2f;

    private float lastTimeShot = 0.0f;

    [SerializeField]
    private GameObject fire;

    private bool usingMouse = false;

    private Vector3 lastMousePosition;

    [SerializeField]
    private Animator animator;

	// Use this for initialization
	void Start () {
        lastMousePosition = GameCamera.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
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
        lastMousePosition = GameCamera.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
    }
	
	// Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = GameCamera.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
        CheckController(mousePosition);

        if(this.usingMouse && GameCamera.Instance.IsInside(mousePosition))
        {
            mousePosition.z = transform.position.z;
            Vector3 direction = Vector3.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
            if (direction.x < GameCamera.Instance.Bounds.min.x || direction.x > GameCamera.Instance.Bounds.max.x)
            {
                direction.x = transform.position.x;
            }
            if (direction.y < GameCamera.Instance.Bounds.min.y || direction.y > GameCamera.Instance.Bounds.max.y)
            {
                direction.y = transform.position.y;
            }

            float xSpeed = direction.x - transform.position.x;

            animator.SetFloat("xSpeed", xSpeed);
            transform.position = direction;
        }
        else
        {
            Vector3 translate = Vector3.zero;
            if (Input.GetKey("left") && transform.position.x > GameCamera.Instance.Bounds.min.x)
            {
                translate += Vector3.left * speed * Time.deltaTime;
            }
            if (Input.GetKey("right") && transform.position.x < GameCamera.Instance.Bounds.max.x)
            {
                translate += Vector3.right * speed * Time.deltaTime;
            }
            if (Input.GetKey("up") && transform.position.y < GameCamera.Instance.Bounds.max.y)
            {
                translate += Vector3.up * speed * Time.deltaTime;
            }
            if (Input.GetKey("down") && transform.position.y > GameCamera.Instance.Bounds.min.y)
            {
                translate += Vector3.down * speed * Time.deltaTime;
            }

            animator.SetFloat("xSpeed", translate.x);
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
