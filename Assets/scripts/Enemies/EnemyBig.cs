using UnityEngine;
using System.Collections;

public class EnemyBig : Enemy {

    [SerializeField]
    private float xSpeed;

    [SerializeField]
    private float shotSpeed;

    [SerializeField]
    private float ySpeed;

    private bool isMovingY = true;

    [SerializeField]
    private float distanceToShoot;

    [SerializeField]
    private float shootIntervalTime;

    private bool dead = false;


	// Use this for initialization
	void Start () {
        // Randomize the x Position when the enemy is created
        float yPosition = GameCamera.Instance.Bounds.min.y - Sprite.bounds.size.y;
        float xPosition = Random.Range(GameCamera.Instance.Bounds.min.x * 1.1f, GameCamera.Instance.Bounds.max.x * 0.9f);
        transform.position = new Vector3(xPosition, yPosition, transform.position.z);
	}

    IEnumerator StartShooting()
    {
        while(true)
        {
            yield return new WaitForSeconds(shootIntervalTime);
            Shoot(shotSpeed, (Player.Instance.transform.position - transform.position).normalized);
        }
    }

    public override void AutoDestroy()
    {
        dead = true;
        this.animator.SetBool("dead", dead);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {

        if(dead)
        {
            return;
        }

        Vector3 direction = Vector3.zero;
        if(this.isMovingY)
        {
            direction = new Vector3(0, ySpeed, 0);
            transform.Translate(direction * Time.deltaTime);
        }
        else
        {
            float xPosition = Mathf.Lerp(transform.position.x,Player.Instance.transform.position.x,Time.deltaTime);
            transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
        }

        if (transform.position.y > GameCamera.Instance.Bounds.max.y * 0.8f && isMovingY)
        {
            isMovingY = false;
            StartCoroutine(StartShooting());
        }
	}
}
