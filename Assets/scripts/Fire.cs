using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private float speed = 3.0f;

    [SerializeField]
    private float power;
    public float Power
    {
        get { return this.power; }
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (GameCamera.Instance.OutOfBounds(sprite.bounds, EDirection.Up))
        {
            Destroy(gameObject);
        }
	}
}
