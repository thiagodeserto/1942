using UnityEngine;
using System.Collections;

public class Scenario : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        transform.localPosition = new Vector3(GameCamera.Instance.Bounds.min.x - (sprite.bounds.size.x / Random.Range(1.0f, 1.9f)), transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	    if(GameCamera.Instance.OutOfBounds(sprite.bounds,EDirection.Down))
        {
            ParallaxManager.Instance.RemoveScenario(this);
        }
	}
}
