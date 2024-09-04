using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Basic_AI_Movement : MonoBehaviour
{
    Animator animator;

    // speed of the AI
    public float speed = 0.5f;

    // position stop
    Vector3 stopPosition;

    // walk and wait time
    float walkTime;
    public float walkCounter;
    float waitTime;
    public float waitCounter;

    // walk direction
    int walkDirection;

    // is the AI walking
    public bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        walkTime = Random.Range(3, 6);
        waitTime = Random.Range(5, 7);

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // if the AI is walking
        if(isWalking)
        {
            animator.SetBool("isRunning", true);

            walkCounter -= Time.deltaTime;

            switch(walkDirection)
            {
                case 0:
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    transform.position += transform.forward * speed * Time.deltaTime;
                    break;
                case 1:
                    transform.localRotation = Quaternion.Euler(0, 90, 0);
                    transform.position += transform.forward * speed * Time.deltaTime;
                    break;
                case 2:
                    transform.localRotation = Quaternion.Euler(0, -90, 0);
                    transform.position += transform.forward * speed * Time.deltaTime;
                    break;
                case 3:
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    transform.position += transform.forward * speed * Time.deltaTime;
                    break;
            }

            if(walkCounter <= 0 )
            {
                stopPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                isWalking = false;

                // stop movement
                transform.position = stopPosition;
                animator.SetBool("isRunning", false);

                // reset waitCounter
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            if(waitCounter <= 0)
            {
                ChooseDirection();
            }
        }
    }

    void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);

        isWalking = true;
        walkCounter = walkTime;
    }
}
