using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AI : Player
{
    [SerializeField]
    int k = 10;

    int i = 0;
    Vector3 moveDir;
    void Awake()
    {
        var random = new System.Random();
        moveDir = new Vector3(random.Next(100) - 50, random.Next(100) - 50).normalized;
        i++;
    }
    void Update()
    {
        if (i % k == 0)
        {
            var random = new System.Random();
            moveDir = new Vector3(random.Next(100) - 50, random.Next(100) - 50).normalized;
        }
        transform.position += moveDir * speed * Time.deltaTime;
        i++;
        if (pick != null)
        {
            pick.transform.position += moveDir * speed * Time.deltaTime;
            if (Math.Abs(moveDir.x * speed * Time.deltaTime + pick.transform.position.x - transform.position.x) > Math.Abs(pick.transform.position.x - transform.position.x)
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

            if (UnityEngine.Random.value < 0.4)
            {
                pick.father = null;
                pick.rigidbody.velocity = moveDir * power;
                pick = null;
            }
        }

    }
}
