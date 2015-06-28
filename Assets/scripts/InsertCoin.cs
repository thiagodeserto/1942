using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InsertCoin : MonoBehaviour {

    [SerializeField]
    private Text text;

    [SerializeField]
    private float OnTime;

    [SerializeField]
    private float OffTime;

	// Use this for initialization
	void Start () {
        StartCoroutine(Blink());
	}
	
    IEnumerator Blink()
    {
        while(true)
        {
            text.enabled = true;
            yield return new WaitForSeconds(OnTime);
            text.enabled = false;
            yield return new WaitForSeconds(OffTime);
        }
    }
}
