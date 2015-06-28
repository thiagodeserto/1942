using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer sprite;

    private Vector3 direction = Vector3.zero;

    private float speed = 0.0f;
	
    public void FillData(float s, Vector3 d)
    {
        this.direction = d;
        this.speed = s;
    }

	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed * Time.deltaTime);
        if (GameCamera.Instance.OutOfBounds(sprite.bounds, EDirection.Down))
        {
            Destroy(gameObject);
        }
	}
}
