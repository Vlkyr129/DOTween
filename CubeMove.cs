using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeMove : MonoBehaviour
{
    public GameObject Cube;
    public Rigidbody rb;
    bool moveRight = true;
    bool isGrounded;

    public bool isCube = false;
    public bool isWall = false;
    public bool isGround = false;

    public float wallRotateSpeed;
    public float wallRotateSpeedScaling = .01f;

    public float jumpPower;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isCube == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
            {
                rb.velocity = new Vector3(0, jumpPower, 0);
                transform.DOPunchScale(new Vector3(0, .8f, 0), 1f, 1);
                Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
                GetComponent<Renderer>().material.color = newColor;
                isGrounded = false;
            }
            
        }
        if (isWall == true)
        {
            transform.Rotate(new Vector3(0, 1, 0) * wallRotateSpeed * Time.deltaTime);
            wallRotateSpeed = wallRotateSpeed + wallRotateSpeedScaling;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(Cube, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Cube")
        {
            if(isWall == true)
            {
                transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 2f, 2);
            }
            if(isGround == true)
            {
                transform.DOPunchScale(new Vector3(0.01f, 0.01f, 0.01f), .1f, 1);
                //transform.DOPunchPosition(new Vector3(0, -1, 0), .1f, 1);
            }

        }

        if (col.gameObject.tag == "Ground")
        {
            transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), .1f, 1);
            isGrounded = true;
        }

        if (col.gameObject.tag == "Wall" && isGround == false)
        {
            Destroy(gameObject);
        }
    }

}
