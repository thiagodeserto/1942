using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxManager : Singleton<ParallaxManager>
{
    [SerializeField]
    private List<Parallax> parallaxList = new List<Parallax>();

    [SerializeField]
    private float speed;
    public float Speed
    {
        set
        {
            speed = value;
        }
    }

    void Start()
    {
        for(int i = parallaxList.Count-1; i > 0; i--)
        {
            AdjustParallaxPosition(parallaxList[i],parallaxList[i-1]);
        }
    }

    private void AdjustParallaxPosition(Parallax first, Parallax second)
    {
        first.transform.localPosition = new Vector3(first.transform.localPosition.x, second.GetBounds.max.y - 1, first.transform.localPosition.z);
    }

	void Update () {
	    for(int i = 0; i < parallaxList.Count; i++)
        {
            Vector3 movement = Vector3.down * speed * Time.deltaTime;
            parallaxList[i].transform.localPosition += movement;
            if(GameCamera.Instance.OutOfBounds(parallaxList[i].GetBounds, EDirection.Down))
            {
                Parallax nextParallax = (i == 0 ? parallaxList[parallaxList.Count - 1] : parallaxList[i - 1]);
                AdjustParallaxPosition(parallaxList[i], nextParallax);
            }
        }
	}
}
