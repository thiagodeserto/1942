using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer sprite;

    public SpriteRenderer Sprite()
    {
        return this.sprite;
    }

    void Update()
    {
        if (GameCamera.Instance.OutOfBounds(sprite.bounds, EDirection.Down))
        {
            ParallaxManager.Instance.RemoveMovable(this);
        }
    }
}
