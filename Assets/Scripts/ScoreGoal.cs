using UnityEngine;
using System.Collections;

public class ScoreGoal : MonoBehaviour {

    public GameObject gameManager;
    GameManager manager;
	
    void Awake()
    {
       // manager = gameManager.GetComponent<GameManager>();
    }

	// Update is called once per frame
	void FixedUpdate () {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "left goal")
        {
            Debug.Log("Right team goal");
            //add goal for right
            gameManager.GetComponent<GameManager>().score("right");
        }
        else
        if (other.gameObject.tag == "right goal")
        {
            Debug.Log("Left team goal");
            //add goal
            gameManager.GetComponent<GameManager>().score("left");
        }     
       
    }


}
