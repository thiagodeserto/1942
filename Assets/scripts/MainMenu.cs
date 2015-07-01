using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void Update () {
	    if(Input.anyKeyDown)
        {
            // TODO: Implement a proper loading
            Application.LoadLevel("main");
        }
	}
}
