using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer sprite;

    void Start()
    {
        ParallaxManager.Instance.AddMovable(this);
        OnStart();
    }

    void Update()
    {
        if (GameCamera.Instance.OutOfBounds(sprite.bounds, EDirection.Down))
        {
            ParallaxManager.Instance.RemoveMovable(this);
        }
        OnUpdate();
    }

    public SpriteRenderer Sprite()
    {
        return this.sprite;
    }

    public virtual void OnStart() { }

    public virtual void OnUpdate() { }
}
