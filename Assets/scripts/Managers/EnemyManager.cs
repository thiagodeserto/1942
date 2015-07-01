using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemyAppearanceData
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
        // Sort the enemy appearance time in case designers don't insert enemies on order
        enemyAppearanceList.Sort((a, b) => { return Mathf.RoundToInt(a.time - b.time); });

        ItemManager.Instance.OnCreateItemEvent += OnCreateItem;
	}

    // Created only for demonstration, not even using Item in this case
    void OnCreateItem(Item item)
    {
        ItemManager.Instance.OnCreateItemEvent -= OnCreateItem;
        if(enemyAppearanceList.Count > 0)
        {
            this.enemyAppearanceList[0].time += 5.0f;
        }
    }

    IEnumerator GenerateEnemy(EnemyAppearanceData enemyAppearanceData)
    {
        for (int i = 0; i < enemyAppearanceData.quantity; i ++)
        {
            yield return new WaitForSeconds(Random.Range(enemyAppearanceData.timeBetween.min, enemyAppearanceData.timeBetween.max));
            Vector3 randomPosition = new Vector3(Random.Range(GameCamera.Instance.Bounds.min.x, GameCamera.Instance.Bounds.max.x), transform.position.y, transform.position.z);
            Enemy selectedEnemy = enemiesList.Find(enemy => enemy.EnemyType == enemyAppearanceData.type);
            // TODO: Implement a pool manager ASAP
            // Not used for this case to avoid using third party pool managers and lose time implementing
            GameObject newEnemy = Instantiate(selectedEnemy.gameObject, randomPosition, Quaternion.identity) as GameObject;
            newEnemy.transform.SetParent(transform);
        }
    }
	
	void Update () {

        if (this.enemyAppearanceList.Count > 0)
        {
            if (this.enemyAppearanceList[0].time < Time.timeSinceLevelLoad)
            {
                StartCoroutine(GenerateEnemy(enemyAppearanceList[0]));
                enemyAppearanceList.RemoveAt(0);
            }
        }
	}
}
