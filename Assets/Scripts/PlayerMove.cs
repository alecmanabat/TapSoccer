using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public float jumpVelocity = 20;
    Rigidbody2D player;
    public bool isGrounded = false;
    GameObject balloon, leg;
    HingeJoint2D hinge;
    JointAngleLimits2D limits;

    //runs once
    void Start()
    {
        player = this.GetComponent<Rigidbody2D>();
        if (this.gameObject.tag=="left player")
            player.centerOfMass += (new Vector2(0.13f, -0.3f));
        else
        if (this.gameObject.tag == "right player")
            player.centerOfMass += (new Vector2(-0.13f, -0.3f));
        balloon = this.transform.FindChild("Balloon").gameObject;
        leg = this.transform.FindChild("leg").gameObject;
        hinge = leg.GetComponent<HingeJoint2D>();
        limits = leg.GetComponent<HingeJoint2D>().limits;
        limits.max = 0;
        hinge.limits = limits;
    }

    //constantly checks for collision with ground
    void OnCollisionEnter2D(Collision2D coll)
    {
        //when player hits ground
        if (coll.gameObject.tag == "ground")
        {
            isGrounded = true;
            balloon.GetComponent<Rigidbody2D>().mass = 0.5f;
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -5));
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x * .9f, 0);

        }
    }

    //constantly runs
    void Update()
    {
        //for testing
        if (GameManager.twoButton == false)
        {
            if (Input.GetButton("Fire1") && (player.gameObject.tag == "left player" || player.gameObject.tag == "left player 2"))
            {
                Jump();
                kickLeg();
            }
            if (Input.GetButtonUp("Fire1") && (player.gameObject.tag == "left player" || player.gameObject.tag == "left player 2"))
            {
                returnLeg();
            }
            if (Input.GetButton("Fire2") && (player.gameObject.tag == "right player" || player.gameObject.tag == "right player 2"))
            {
                Jump();
                kickLeg();
            }
            if (Input.GetButtonUp("Fire2") && (player.gameObject.tag == "right player" || player.gameObject.tag == "right player 2"))
            {
                returnLeg();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && player.gameObject.tag == "left player")
            {
                Jump();
                kickLeg();
            }
            if (Input.GetButtonUp("Fire1") && player.gameObject.tag == "left player")
            {
                returnLeg();
            }
            if (Input.GetButton("Fire2") && player.gameObject.tag == "left player 2")
            {
                Jump();
                kickLeg();
            }
            if (Input.GetButtonUp("Fire2") && player.gameObject.tag == "left player 2")
            {
                returnLeg();
            }
            if (Input.GetButton("Fire3") && player.gameObject.tag == "right player 2")
            {
                Jump();
                kickLeg();
            }
            if (Input.GetButtonUp("Fire3") && player.gameObject.tag == "right player 2")
            {
                returnLeg();
            }
            if (Input.GetButton("Fire4") && player.gameObject.tag == "right player")
            {
                Jump();
                kickLeg();
            }
            if (Input.GetButtonUp("Fire4") && player.gameObject.tag == "right player")
            {
                returnLeg();
            }
        }
        
    }

    //adds y velocity to object, changes grounded check to false
    public void Jump()
    {
        //checks if on ground to prevent double jumps
        if (isGrounded)
        {
            player.AddRelativeForce(new Vector2(0,jumpVelocity),ForceMode2D.Impulse);
            isGrounded = false;
            balloon.GetComponent<Rigidbody2D>().mass = 0;
            
        }
        //leg.GetComponent<Rigidbody2D>().AddForce(new Vector2(20, 0));
    }

    //keeps leg angle at 90 degrees
    public void kickLeg()
    {
        limits.min = -90;
        limits.max = -90;
        hinge.limits = limits;
    }

    //forces leg angle to 0 degrees
    public void returnLeg()
    {
        limits.min = 0;
        limits.max = 0;
        hinge.limits = limits;
    }
}