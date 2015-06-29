using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct EnemyAppearanceData
{
    public EEnemyType type;
    public int quantity;
    public float time;
    public SimpleRange timeBetween;
}

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private List<EnemyAppearanceData> enemyAppearanceList = new List<EnemyAppearanceData>();

	// Use this for initialization
	void Start () {
        enemyAppearanceList.Sort((a, b) => { return Mathf.RoundToInt(a.time - b.time); });
	}

    IEnumerator GenerateEnemy(EnemyAppearanceData enemyAppearanceData)
    {
        for (int i = 0; i < enemyAppearanceData.quantity; i ++)
        {
            yield return new WaitForSeconds(Random.Range(enemyAppearanceData.timeBetween.min, enemyAppearanceData.timeBetween.max));
            Vector3 randomPosition = new Vector3(Random.Range(GameCamera.Instance.Bounds.min.x, GameCamera.Instance.Bounds.max.x), transform.position.y, transform.position.z);
            GameObject newEnemy = Instantiate(enemy.gameObject, randomPosition, Quaternion.identity) as GameObject;
            newEnemy.transform.SetParent(transform);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
        if(enemyAppearanceList.Count > 0)
        {
            if(enemyAppearanceList[0].time < Time.timeSinceLevelLoad)
            {
                StartCoroutine(GenerateEnemy(enemyAppearanceList[0]));
                enemyAppearanceList.RemoveAt(0);
            }
        }
	}
}
