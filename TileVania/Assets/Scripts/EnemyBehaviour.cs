using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.1f;
    private bool movingRight = true;
    private bool movingLeft = false;
    private FallCheck fallCheck;
    //private FallCheck leftFallCheck;
    void Start()
    {

        fallCheck = gameObject.GetComponentInChildren<FallCheck>();
            //GetComponent<FallCheck>();
        //leftFallCheck = GameObject.Find("LeftFallCheck").GetComponent<FallCheck>();
    }
    void Update()
    {
        if (fallCheck.canFall == true)
        {
            if (transform.localScale.x == 1)
            {
                //Debug.Log("Right ledge detected");
                movingRight = false;
                movingLeft = true;
            }
            else
            {
                //Debug.Log("Left ledge detected");
                movingLeft = false;
                movingRight = true;
            }
        }
            
        /*else if(leftFallCheck.canFall==true)
        {
            Debug.Log("Left ledge detected");
            movingLeft = false;
            movingRight = true;
        }*/



        if(movingRight==true)
        {
            Vector3 rightFlip = new Vector3(1, 1, 1);
            transform.localScale = rightFlip;
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else if(movingLeft==true)
        {
            Vector3 leftFlip = new Vector3(-1, 1, 1);
            transform.localScale = leftFlip;
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
