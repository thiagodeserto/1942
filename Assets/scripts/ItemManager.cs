using UnityEngine;
using System.Collections;

public class ItemManager : Singleton<ItemManager> {

    [SerializeField]
    private Item itemPrefab;

    public void CreateItem(Vector3 position)
    {
        GameObject item = (GameObject)Instantiate(itemPrefab.gameObject, position, Quaternion.identity);
        item.transform.SetParent(transform);
        item.transform.localPosition = new Vector3(item.transform.localPosition.x, item.transform.localPosition.y, 0.0f);
    }
}
