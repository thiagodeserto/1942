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
            Vector3 randomPosition = new Vector3(Random.Range(GameCamera.Instance.Bounds.min.x, GameCamera.Instance.Bounds.max.x),transform.position.y,transform.position.z);
            GameObject newEnemy = Instantiate(enemy.gameObject, randomPosition, Quaternion.identity) as GameObject;
            newEnemy.transform.SetParent(transform);
            enemiesList.Add(newEnemy.GetComponent<Enemy>());
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
