using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private GameObject aiObject;


    GameObject[] teamH, teamA;

    public static Color homeColor, awayColor;



    private void Awake()
    {
        homeColor = Color.blue;
        awayColor = Color.red;
    }
    // Start is called before the first frame update
    void Start()
    {

        
    }



    public void NewGame()
    {
        CloseButton();
        StartGame();
    }

    public void TrainAI()
    {

    }

    void CloseButton()
    {
        foreach (var ob in GetComponentsInChildren<Transform>())
        {
            if (ob.tag == "ButtonStart")
            {
                ob.gameObject.SetActive(false);
            }
        }
    }

    void StartGame()
    {
        teamA = new GameObject[3];
        teamH = new GameObject[3];
        teamA[0] = Instantiate(aiObject);
        teamA[1] = Instantiate(aiObject);
        teamA[2] = Instantiate(aiObject);
        teamA[0].transform.position = new Vector3(-6.8f, 0, -1);
        teamA[1].transform.position = new Vector3(-10f, 2, -1);
        teamA[2].transform.position = new Vector3(-10f, -2, -1);
        foreach (var ob in teamA)
        {
            ob.tag = "AwayPlayers";
        }
        teamH[0] = Instantiate(aiObject);
        teamH[1] = Instantiate(playerObject);
        teamH[2] = Instantiate(aiObject);
        teamH[0].transform.position = new Vector3(-3.2f, 0, -1);
        teamH[1].transform.position = new Vector3(0, 2, -1);
        teamH[2].transform.position = new Vector3(0f, -2, -1);
        foreach (var ob in teamH)
        {
            ob.tag = "HomePlayers";
        }
        foreach (var ob in GameObject.FindGameObjectsWithTag("AwayPlayers"))
        {
            ob.GetComponentInChildren<SpriteRenderer>().color = awayColor;
        }
        foreach (var ob in GameObject.FindGameObjectsWithTag("HomePlayers"))
        {
            ob.GetComponentInChildren<SpriteRenderer>().color = homeColor;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
