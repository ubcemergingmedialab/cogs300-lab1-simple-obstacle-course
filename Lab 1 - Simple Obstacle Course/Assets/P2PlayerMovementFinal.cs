using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class P2PlayerMovementFinal : MonoBehaviour
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
        // TODO: implements movement logic for player object:
        // - Move forward if not dodging or stopping
        // - Move sideways to appropriate side if in a dodging state
        // - Stop moving if in stopping state
        if (!dodging_left && !dodging_right && !stopping) {
            rb.AddForce(0,0,500 * Time.deltaTime);
        }

        else if (dodging_left || dodging_right) {
            if (dodging_left) {
                rb.AddForce(-900,0,0);
                dodging_left = false;
            } 
            else if (dodging_right) {
                rb.AddForce(900,0,0);
                dodging_right = false;
            }
        }

        else if (stopping) {
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
            
            // Change inner states based on obstacle information
            // TODO: implements change of boolean inner states based on obstacle opening
            if (!RightOpen && !LeftOpen) {
                stopping = true;
            } else {
                if (RightOpen) {
                    dodging_right = true;
                } 
                else if (LeftOpen) {
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

       //Call SetColor using the shader property name "_Color" and setting the color based on input
       if (color == "red") {
           playerRenderer.material.SetColor("_Color", Color.red);
       }   
    }
}

