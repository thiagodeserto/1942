using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parallax : Singleton<Parallax> {

    [SerializeField]
    private SpriteRenderer sprite;

    public Bounds GetBounds
    {
        get { return this.sprite.bounds; }
    }
}
