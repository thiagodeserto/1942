﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public struct SimpleRange
{
    public float min;
    public float max;
}

public class EnemySimple : Enemy {

    [SerializeField]
    private float xSpeed;

    [SerializeField]
    private float ySpeed;
    private float ySpeedInverted;

    [SerializeField]
    private SimpleRange playerDistanceToShoot;

    private float distanceToShoot;

    private bool isGoing = true;

	// Use this for initialization
	void Start () {

        this.distanceToShoot = Random.Range(playerDistanceToShoot.min, playerDistanceToShoot.max);

        if(transform.position.x > GameCamera.Instance.Bounds.center.x)
        {
            xSpeed *= -1;
        }

        this.ySpeedInverted = -this.ySpeed;
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 direction = new Vector3(xSpeed, ySpeed, 0);
        transform.Translate(direction * Time.deltaTime);

        if (transform.position.y < this.distanceToShoot && isGoing)
        {
            isGoing = false;
            this.animator.SetBool("isGoing", isGoing);
        }

        // Interpolates the enemy speed to better animation
        if(!isGoing)
        {
            this.ySpeed = Mathf.Lerp(this.ySpeed,this.ySpeedInverted,Time.deltaTime * 3.0f);
        }
	}
}
