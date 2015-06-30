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
    private List<Enemy> enemiesList = new List<Enemy>();

    [SerializeField]
    private List<EnemyAppearanceData> enemyAppearanceList = new List<EnemyAppearanceData>();

	void Start () {
        enemyAppearanceList.Sort((a, b) => { return Mathf.RoundToInt(a.time - b.time); });
	}

    IEnumerator GenerateEnemy(EnemyAppearanceData enemyAppearanceData)
    {
        for (int i = 0; i < enemyAppearanceData.quantity; i ++)
        {
            yield return new WaitForSeconds(Random.Range(enemyAppearanceData.timeBetween.min, enemyAppearanceData.timeBetween.max));
            Vector3 randomPosition = new Vector3(Random.Range(GameCamera.Instance.Bounds.min.x, GameCamera.Instance.Bounds.max.x), transform.position.y, transform.position.z);
            Enemy selectedEnemy = enemiesList.Find(enemy => enemy.EnemyType == enemyAppearanceData.type);
            GameObject newEnemy = Instantiate(selectedEnemy.gameObject, randomPosition, Quaternion.identity) as GameObject;
            newEnemy.transform.SetParent(transform);
        }
    }
	
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
