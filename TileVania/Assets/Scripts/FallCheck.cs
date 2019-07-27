using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCheck : MonoBehaviour
{
    public bool canFall = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Ground")
        {
            canFall = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {     
        if (collision.tag == "Ground")
        {
            canFall = true;
        }
    }
}
