using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GameManager : MonoBehaviour {
    public GameObject P1, legP1, P2, legP2, P3, legP3, P4, legP4, ball;
    Vector3 origP1Pos, origLegP1Pos, origP2Pos, origLegP2Pos, origP3Pos, origLegP3Pos, origP4Pos, origLegP4Pos, origBallPos, resetBallPos;
    public int left = 0, right = 0;
    public Text leftScore, rightScore;
    public Button B1, B2, B3, B4;
    public static bool twoButton;
    // Use this for initialization
    void Awake ()
    {
        origBallPos = ball.transform.position;
        origP1Pos = P1.transform.position;
        origLegP1Pos = legP1.transform.position;
        origP2Pos = P2.transform.position;
        origLegP2Pos = legP2.transform.position;
        origP3Pos = P3.transform.position;
        origLegP3Pos = legP3.transform.position;
        origP4Pos = P4.transform.position;
        origLegP4Pos = legP4.transform.position;


    }
	
    void Start()
    {
        leftScore.text = "" + left;
        rightScore.text = "" + right;

        if (!twoButton)
        {
            B3.gameObject.SetActive(false);
            B4.gameObject.SetActive(false);

            EventTrigger trigger = B1.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.InitializePotentialDrag;
            entry.callback.AddListener((eventData) => { P3.GetComponent<PlayerMove>().Jump(); });

            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerUp;
            entry2.callback.AddListener((eventData) => { P3.GetComponent<PlayerMove>().returnLeg(); });

            EventTrigger.Entry entry3 = new EventTrigger.Entry();
            entry3.eventID = EventTriggerType.PointerDown;
            entry3.callback.AddListener((eventData) => { P3.GetComponent<PlayerMove>().kickLeg(); });
            trigger.triggers.Add(entry);
            trigger.triggers.Add(entry2);
            trigger.triggers.Add(entry3);

            EventTrigger trigger2 = B2.GetComponent<EventTrigger>();
            EventTrigger.Entry entry4 = new EventTrigger.Entry();
            entry4.eventID = EventTriggerType.InitializePotentialDrag;
            entry4.callback.AddListener((eventData) => { P4.GetComponent<PlayerMove>().Jump(); });

            EventTrigger.Entry entry5 = new EventTrigger.Entry();
            entry5.eventID = EventTriggerType.PointerUp;
            entry5.callback.AddListener((eventData) => { P4.GetComponent<PlayerMove>().returnLeg(); });

            EventTrigger.Entry entry6 = new EventTrigger.Entry();
            entry6.eventID = EventTriggerType.PointerDown;
            entry6.callback.AddListener((eventData) => { P4.GetComponent<PlayerMove>().kickLeg(); });
            trigger2.triggers.Add(entry4);
            trigger2.triggers.Add(entry5);
            trigger2.triggers.Add(entry6);
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (ball.transform.position.x > 11.2)
        {
            resetAll("left");
        }
        else
    if (ball.transform.position.x < -11.2)
        {
            resetAll("right");
        }
    }

    //returns object to original position
    public void reset(GameObject obj, Vector3 origPos)
    {
        obj.transform.position = origPos;
        obj.transform.rotation = Quaternion.identity;
        obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        obj.GetComponent<Rigidbody2D>().angularVelocity = 0;
    }

    public void resetAll(string side)
    {
        reset(P1, origP1Pos);
        reset(legP1, origLegP1Pos);
        reset(P2, origP2Pos);
        reset(legP2, origLegP2Pos);
        reset(P3, origP3Pos);
        reset(legP3, origLegP3Pos);
        reset(P4, origP4Pos);
        reset(legP4, origLegP4Pos);
        resetBallPos = origBallPos;
        resetBall(side);
    }

    public void resetBall(string side)
    {
        if (side == "left")
        {
            resetBallPos.x *= -1;
        }
        reset(ball, resetBallPos);
        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        ball.GetComponent<Rigidbody2D>().gravityScale = 0;
        yield return new WaitForSeconds(1);
        ball.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    IEnumerator restart()
    {
        yield return new WaitForSeconds(3);
    }

    //when the ball enters a goal, resets all players and ball
    //puts ball on side of team that was scored on
    public void score(string side)
    {
        StartCoroutine(delay());

        if (side == "left")
        {
            left++;
            if (left==5)
            {
                //left team wins
                StartCoroutine(restart());
                left = 0;
                right = 0;
                leftScore.text = "" + left;
                rightScore.text = "" + right;

            }
            leftScore.text = "" + left;
            rightScore.text = "" + right;
        }
        else
        {
            right++;
            if (right == 5)
            {
                //right team wins
                StartCoroutine(restart());
                left = 0;
                right = 0;
                leftScore.text = "" + left;
                rightScore.text = "" + right;
            }
            leftScore.text = "" + left;
            rightScore.text = "" + right;
        }
        resetAll(side);
        Debug.Log("Left: " + left + "    Right: " + right);

    }

}
