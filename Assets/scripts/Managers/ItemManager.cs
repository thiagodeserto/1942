using UnityEngine;
using System.Collections;

public class ItemManager : Singleton<ItemManager> {

    [SerializeField]
    private Item itemPrefab;

    private int lastScoreItem = 0;

    public delegate void OnCreateItem(Item item);
    public event OnCreateItem OnCreateItemEvent;

    public void CheckScoreItem(Vector3 position)
    {
        int floordiv = Mathf.FloorToInt(Player.Instance.Score / 3000);
        if(floordiv > lastScoreItem)
        {
            lastScoreItem = floordiv;
            CreateItem(position);
        }
    }

    public void CreateItem(Vector3 position)
    {
        GameObject item = (GameObject)Instantiate(itemPrefab.gameObject, position, Quaternion.identity);
        item.transform.SetParent(transform);
        item.transform.localPosition = new Vector3(item.transform.localPosition.x, item.transform.localPosition.y, 0.0f);
        if(OnCreateItemEvent != null)
        {
            OnCreateItemEvent(item.GetComponent<Item>());
        }
    }
}
