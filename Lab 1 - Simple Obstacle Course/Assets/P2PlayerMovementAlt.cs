using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class P2PlayerMovementAlt : MonoBehaviour
{
    // VARIABLE DECLARATIONS
    public Rigidbody rb; // refers to the player GameObject itself
    public bool dodging_left = false;  // indicates whether the player object is in the process of dodging an obstacle leftwards
    public bool dodging_right = false;  // indicates whether the player object is in the process of dodging an obstacle rightwards
    public bool stopping = false; // indicates whether the player object is stopping

    // FUNCTIONS
    
    // This function is called once per frame. This is the basic function for making any real-time changes/movement of an object
    void Update()
    {
        // Box moves forward if it is not dodging an obstacle or stopping 
        // TODO: implements move forward logic

        if (!dodging_left && !dodging_right && !stopping) {
            rb.AddForce(0,0,500 * Time.deltaTime);
        }

        // Box moves sideways if it is in dodging state

        if (dodging_left) {
            rb.AddForce(-500,0,0);
            dodging_left = false;
        } else if (dodging_right) {
            rb.AddForce(500,0,0);
            dodging_right = false;
        }

        // Box stops moving if it is in stopping state
        if (stopping) {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            ChangeColor("red");  // OPTIONAL color change
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

            if (!RightOpen && !LeftOpen) {
                stopping = true;
            } else {
                if (RightOpen) {
                    dodging_right = true;
                } else if (LeftOpen) {
                    dodging_left = true;
                }
            }

        }
    }

    // BONUS: implement this function which changes the color of the box
    // Once implemented, try calling this function at different places in the code to see
    // how the color change reflects the internal movement logic of the box
    void ChangeColor(string color) {
        var playerRenderer = rb.GetComponent<Renderer>();

       //Call SetColor using the shader property name "_Color" and setting the color to red
       playerRenderer.material.SetColor("_Color", Color.red);        
    }
}

