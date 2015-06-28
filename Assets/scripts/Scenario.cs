using UnityEngine;
using System.Collections;

public class Scenario : Movable {

    [SerializeField]
    private EDirection direction;

	// Use this for initialization
	public override void OnStart () {
        float x = transform.localPosition.x;
        if (direction == EDirection.Left)
        {
            x = GameCamera.Instance.Bounds.min.x - (Sprite().bounds.size.x / Random.Range(1.0f, 2.0f));
        }
        else if(direction == EDirection.Right)
        {
            transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
            x = GameCamera.Instance.Bounds.max.x + (Sprite().bounds.size.x / Random.Range(1.0f, 2.0f));
        }
        transform.localPosition = new Vector3(x, transform.position.y, transform.position.z);
	}
}
