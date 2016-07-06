using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleTwoButton : MonoBehaviour {

    public GameObject toggle;
	// Use this for initialization
	void Start ()
    {
         
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (toggle.GetComponent<Toggle>().isOn == false)
        {
            GameManager.twoButton = false;
        }
        else
            GameManager.twoButton = true;
    }
}
