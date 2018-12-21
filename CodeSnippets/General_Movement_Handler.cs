using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Move_Com;
using State_Commands;
using PlayerStateMachine;

public class General_Movement_Handler : StateCommand
{

    // === create commands to move left and right ===
    private Movement_Command_Base comMoveLeft;
    private Movement_Command_Base comMoveRight;

    // === create commands to move left and right ===
    public General_Movement_Handler()
    {
        comMoveLeft = new Move_Left_State();
        comMoveRight = new Move_Right_State();
    }

    public override void UpdatePlayer(ref Rigidbody2D r, ref StateCommand c)
    {

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Y is clicked");
            if (Player_State_Base.isCollidingHeld == true)
            {
                c = Player_State_Base.pGrab;
                Debug.Log("State changed to Grab");
            }
            else if (Player_State_Base.isCollidingAffected == true && (Player_State_Base.currentHeldObject is torchHeldObjectCommand))
            {
                c = Player_State_Base.pMelt;
            }
            else if (Player_State_Base.isCollidingHeld == false && (Player_State_Base.currentHeldObject is torchHeldObjectCommand))
            {
                c = Player_State_Base.pDrop;
            }
        }
    }

    public override void Collide2DEnter(ref Rigidbody2D r, ref Collision2D collision, ref StateCommand c)
    {
        // === push pull object ===
        if (collision.gameObject.tag == "PushPullGameObjects")
        {
            c = Player_State_Base.pPushPull;
            Debug.Log("State changed to PushPull");
        }
        else if (collision.gameObject.tag == "Climbable")
        {
            c = Player_State_Base.pClimb;
            Debug.Log("State changed to Climb");
        }
    }

    public override void Collide2DExit(ref Rigidbody2D r, ref Collision2D collision, ref StateCommand c)
    {

    }

    public override void Trigger2DEnter(ref Rigidbody2D r, ref Collider2D collider, ref StateCommand c)
    {
        Debug.Log("TriggerEnter being called");

    }

    public override void Trigger2DStay(ref Rigidbody2D r, ref Collider2D collider, ref StateCommand c)
    {
        if (collider.gameObject.tag == "Grabable")
        {
            Player_State_Base.isCollidingHeld = true;
            Player_State_Base.heldDetected = collider.gameObject;
        }
        if (collider.gameObject.tag == "Affectable")
        {
            Debug.Log("TriggerEnter:  Affectable Object Nearby");
            Player_State_Base.isCollidingAffected = true;
            Player_State_Base.affectableDetected = collider.gameObject;
        }

    }

    public override void Trigger2DExit(ref Rigidbody2D r, ref Collider2D collider, ref StateCommand c)
    {
        Debug.Log("TriggerExit being called");
        Player_State_Base.isCollidingHeld = false;
        Player_State_Base.isCollidingAffected = false;

        Player_State_Base.heldDetected = null;
        Player_State_Base.affectableDetected = null;

    }
}
