using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private List<Enemy> enemiesList = new List<Enemy>();

	// Use this for initialization
	void Start () {
        StartCoroutine(GenerateEnemy());
	}

    IEnumerator GenerateEnemy()
    {
        while(true)
        {
            yield return new WaitForSeconds(2.0f);
            GameObject newEnemy = Instantiate(enemy.gameObject,transform.position,Quaternion.identity) as GameObject;
            enemiesList.Add(newEnemy.GetComponent<Enemy>());
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
