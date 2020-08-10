using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class P2PlayerMovementFinal : MonoBehaviour
{
    // VARIABLE DECLARATIONS
    public Rigidbody rb; // refers to the player GameObject itself
    public bool dodging = false;  // indicates whether the player object is in the process of dodging an obstacle
    public bool stopping = false; // indicates whether the player object is stopping

    
    // FUNCTIONS
    
    // This function is called once per frame. This is the basic function for making any real-time changes/movement of an object
    void Update()
    {
        // Box moves forward if it is not dodging an obstacle or stopping
        // TODO: implements move forward logic 
        if (!stopping & !dodging) {
            rb.AddForce(0, 0, 500 * Time.deltaTime);
            ChangeColor("green");  // OPTIONAL color change
        }        
    }

    // This function is called whenever the box collides with an object (including the ground)
    void OnCollisionEnter(Collision collisionInfo) {
        // Only proceed if collided object is an obstacle 
        if (collisionInfo.collider.tag == "Obstacle") {        
            
            // RightOpen indicates whether the obstacle's right side is open
            bool RightOpen = collisionInfo.collider.gameObject.GetComponent<ObstacleInfo>().RightOpen;
            // LeftOpen indicates whether the obstacle's left side is open
            bool LeftOpen = collisionInfo.collider.gameObject.GetComponent<ObstacleInfo>().LeftOpen;
            
            // The box should dodge the obstacle if either the right or left side is open, otherwise stop moving
            // TODO: implements maneuver logic
            if (RightOpen || LeftOpen) {
                Dodge(RightOpen, LeftOpen);
            }
            else {
                Stop();
            }          
        }
    }

    // This function is the dodge maneuver i.e. it is called when you want the box to dodge
    // The input parameters are indicators of whether the right and left side of the obstacle is open
    // The box should dodge to the right if the right side is open and vice versa
    // TODO: implements this function 
    void Dodge(bool RightOpen, bool LeftOpen) {
        dodging = true;

        if (RightOpen) {
            DodgeRight();
        }
        else {
            DodgeLeft();
        }

        dodging = false;
    }

    // This function is the stop maneuver i.e. it is called when you want the box to stop
    void Stop() {
        stopping = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        ChangeColor("red");  // OPTIONAL color change
    }

    // The dodge to the right maneuver
    // TODO: implement this function
    void DodgeRight() {
        rb.AddForce(900, 0, 0);
    }

    // The dodge to the left maneuver
    // TODO: implement this function
    void DodgeLeft() {
        rb.AddForce(-900, 0, 0);
    }

    // BONUS: implement this function which changes the color of the box
    // Once implemented, try calling this function at different places in the code to see
    // how the color change reflects the internal movement logic of the box
    void ChangeColor(string color) {
        if (color == "red") {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (color == "green") {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        else if (color == "yellow") {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (color == "blue") {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        } 
    }
}

