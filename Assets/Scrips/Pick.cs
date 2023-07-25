using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    private float speed = 0f;
    public Player father = null;
    public Rigidbody2D rigidbody;

    public delegate void TeamGoal(int team);// 0 = home, 1=away

    public event TeamGoal? Goal;

    public static Pick Instance { get; private set; }

    private void Awake()
    {
        Instance = this; 
        rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Score")
        {
            Debug.Log("Goal");
            Goal?.Invoke((collision.name == "gateScoreRight" ? 1 : 0));
            transform.position = new Vector3(-5, 0, -1);
            rigidbody.velocity = Vector3.zero;
        }
    }


}
