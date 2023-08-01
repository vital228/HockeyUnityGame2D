using Assets.Scrips;
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


    public GameObject[] teammates;



    public GameObject[] skins;

    public Pick pick = null;
    // Start is called before the first frame update
    protected void Start()
    {

    }

    protected void Awake()
    {
        teammates = new GameObject[Settings.countPlayersInTeam - 1];
    }

    public void AddTeammate(GameObject t, int index)
    {
        teammates[index] = t;
    }


    private void Swap(GameObject a, GameObject b) { 
        var t = a.transform.position;
        a.transform.position = b.transform.position;
        b.transform.position = t;

        var t1 = a.transform.rotation;
        a.transform.rotation = b.transform.rotation;
        b.transform.rotation = t1;
    }

    private float GetDistanceToPick(GameObject t)
    {
        return Vector3.Distance(t.transform.position, Pick.Instance.transform.position);
    }

    private void SwapTeammate()
    {
        int index = -1;
        for (int i = 0; i < teammates.Length; i++)
        {
            if (index == -1)
            {
                index = i;
            }
            else
            {
                if (GetDistanceToPick(teammates[index])> GetDistanceToPick(teammates[i]))
                {
                    index = i;
                }
            }
        }
        if (index != -1)
            Swap(gameObject, teammates[index]);
    }
    private void SwapTeammate(int index)
    {
        Swap(gameObject, teammates[index]);
    }


    // Update is called once per frame
    void Update()
    {
        HandleChange();
        HandleMovement();
        HandleShot();
    }

    private void HandleChange()
    {
        if (pick == null && Input.GetKeyUp(KeyCode.Space))
        {
            SwapTeammate();
        }
        if (pick == null)
        {
            for (int i = 0; i < teammates.Length; i++)
            {
                if (teammates[i].GetComponent<AI>().pick != null)
                {
                    SwapTeammate(i);
                    return;
                }
            }
        }
    }

    private void HandleShot()
    {
        if (Input.GetKeyUp(KeyCode.Space) && pick != null)
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
