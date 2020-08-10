using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1PlayerMovementFinal : MonoBehaviour
{
    public Rigidbody rb; // refers to the player GameObject itself
    
    // Update is called once per frame
    void Update()
    {
        rb.AddForce(0, 0, 500 * Time.deltaTime);
    }
}
