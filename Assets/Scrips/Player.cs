using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    protected float speed = 1f;

    [SerializeField]
    protected float speedPick = 1f;

    [SerializeField]
    protected float power = 10f;

    protected float moveX, moveY;

    protected Pick pick = null;
    // Start is called before the first frame update
    protected void Start()
    {
    }

    protected void Awake()
    {
    }


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleShot();
    }

    private void HandleShot()
    {
        if (Input.GetKey(KeyCode.Space) && pick != null)
        {
            Vector3 moveDir = new Vector3(moveX, moveY).normalized;
            pick.father = null;
            pick.rigidbody.velocity = moveDir * power;
            pick = null;
        }
    }

    private void HandleMovement()
    {
        moveX = 0f;
        moveY = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY += 1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveY -= 1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX -= 1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX += 1f;
        }
        Vector3 moveDir = new Vector3(moveX, moveY).normalized;

        transform.position += moveDir * speed * Time.deltaTime;
        if (pick != null)
        {
            pick.transform.position += moveDir * speed * Time.deltaTime;
            if (Math.Abs(moveDir.x * speed * Time.deltaTime + pick.transform.position.x - transform.position.x)>Math.Abs(pick.transform.position.x - transform.position.x)
                && Math.Abs(pick.transform.position.x - transform.position.x) >= 0.6f)
            {
                moveDir.x = 0;
            }
            if (Math.Abs(moveDir.y * speed * Time.deltaTime + pick.transform.position.y - transform.position.y) > Math.Abs(pick.transform.position.y - transform.position.y)
                && Math.Abs(pick.transform.position.y - transform.position.y) >= 0.6f)
            {
                moveDir.y = 0;
            }
            pick.transform.position += moveDir * speed * Time.deltaTime;
        }
    }


    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pick")
        {
            Pick.Instance.father = this;
            pick = collision.gameObject.GetComponent<Pick>();
            pick.rigidbody.velocity = Vector3.zero;
        }
    }
}
