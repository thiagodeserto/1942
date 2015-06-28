using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxManager : Singleton<ParallaxManager>
{
    [SerializeField]
    private List<Parallax> parallaxList = new List<Parallax>();

    [SerializeField]
    private List<Movable> movableObjects = new List<Movable>();

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
        for (int i = 1; i < parallaxList.Count; i++)
        {
            AdjustParallaxPosition(parallaxList[i],parallaxList[i-1]);
        }
    }

    private void AdjustParallaxPosition(Parallax first, Parallax second)
    {
        first.transform.localPosition = new Vector3(first.transform.localPosition.x, second.GetBounds.max.y - 1, first.transform.localPosition.z);
    }

    public void RemoveMovable(Movable movable)
    {
        this.movableObjects.Remove(movable);
    }

    public void AddMovable(Movable movable)
    {
        this.movableObjects.Add(movable);
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

        for (int i = 0; i < movableObjects.Count; i++)
        {
            Vector3 movement = Vector3.down * speed * Time.deltaTime;
            movableObjects[i].transform.localPosition += movement;
        }
	}
}
