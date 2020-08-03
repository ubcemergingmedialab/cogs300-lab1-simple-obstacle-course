using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public bool dodging = false;

    // Update is called once per frame
    void Update()
    {
        if (!dodging) {
            rb.AddForce(0, 0, 500 * Time.deltaTime);
        }
        
    }

    void OnCollisionEnter(Collision collisionInfo) {
        if (collisionInfo.collider.tag == "Obstacle") {
            dodging = true;
            bool RightOpen = collisionInfo.collider.gameObject.GetComponent<ObstacleInfo>().RightOpen;
            if (RightOpen) {
                DodgeRight();
            }
            else {
                DodgeLeft();
            }
            dodging = false;
        }

    }

    void DodgeRight() {
        rb.AddForce(900, 0, 0);
    }

    void DodgeLeft() {
        rb.AddForce(-900, 0, 0);
    }
}
