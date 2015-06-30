using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private float speed = 3.0f;

    [SerializeField]
    private int power;
    public int Power
    {
        get { return this.power; }
    }
	
	void Update () {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (GameCamera.Instance.OutOfBounds(sprite.bounds, EDirection.Up))
        {
            Destroy(gameObject);
        }
	}
}
