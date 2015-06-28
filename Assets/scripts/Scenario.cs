using UnityEngine;
using System.Collections;

public class Scenario : Movable {

	// Use this for initialization
	void Start () {
        transform.localPosition = new Vector3(GameCamera.Instance.Bounds.min.x - (Sprite().bounds.size.x / Random.Range(1.0f, 1.9f)), transform.position.y, transform.position.z);
	}
}
