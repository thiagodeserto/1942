using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
