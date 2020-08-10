using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variable Declarations
    
    
    public Rigidbody rb;
    public bool dodging = false;
    public bool stopping = false;

    // Update is called once per frame
    void Update()
    {
        if (!stopping & !dodging) {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            rb.AddForce(0, 0, 500 * Time.deltaTime);
        }        
    }

    void OnCollisionEnter(Collision collisionInfo) {
        if (collisionInfo.collider.tag == "Obstacle") {        
            bool RightOpen = collisionInfo.collider.gameObject.GetComponent<ObstacleInfo>().RightOpen;
            bool LeftOpen = collisionInfo.collider.gameObject.GetComponent<ObstacleInfo>().LeftOpen;
            
            if (RightOpen || LeftOpen) {
                Dodge(RightOpen, LeftOpen);
            }
            else {
                Stop();
            }          
        }
    }

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

    void Stop() {
        stopping = true;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void DodgeRight() {
        rb.AddForce(900, 0, 0);
    }

    void DodgeLeft() {
        rb.AddForce(-900, 0, 0);
    }
}
