using UnityEngine;
using System.Collections;

public class Item : Movable {

    void Start()
    {
        ParallaxManager.Instance.AddMovable(this);
    }
}
