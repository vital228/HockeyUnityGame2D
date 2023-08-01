using Assets.Scrips;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private GameObject aiObject;

    [SerializeField]
    private SpriteAtlas atlasForm;

    [SerializeField]
    private GameObject endGamePanel;

    [SerializeField]
    private Text endGameWhoWin;
    [SerializeField]
    private Text endGameScore;




    GameObject[] teamH, teamA;

    public static int homeColor = 0, awayColor = 1;

    public Text textGoalH, textGoalA, period, timeText;

    public Image logoHome, logoAway;


    private int speedPerionInMinute = 1;

    private const int periodMinute = 20;

    private float countdown;

    private int periodNow = 1;








    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        Pick.Instance.Goal += GoalTeam;
        StartGame();
    }

    void GoalTeam(int team)
    {
        if (team + periodNow % 2 == 1)
        {
            textGoalH.text = (Int32.Parse(textGoalH.text)+1).ToString();
        }
        else
        {
            textGoalA.text = (Int32.Parse(textGoalA.text) + 1).ToString();
        }
        ResetPosition();
    }



    void StartGame()
    {
        speedPerionInMinute = Settings.speed;
        homeColor = Settings.homeSkin;
        awayColor = Settings.awaySkin;

        teamA = new GameObject[Settings.countPlayersInTeam];
        teamH = new GameObject[Settings.countPlayersInTeam];
        for (int i = 0; i<Settings.countPlayersInTeam; i++){
            teamA[i] = Instantiate(aiObject);
            teamA[i].transform.position = Settings.startPositionAway[i];
        }
        foreach (var ob in teamA)
        {
            ob.tag = "AwayPlayers";
            var t = ob.transform.GetChild(2).GetChild(0);
            t.GetComponent<SpriteRenderer>().sprite = atlasForm.GetSprite("HockeyTeamNHL_" + awayColor);

        }
        teamH[0] = Instantiate(playerObject);
        teamH[0].transform.position = Settings.startPositionHome[0];
        for (int i = 1; i<Settings.countPlayersInTeam; i++)
        {
            teamH[i] = Instantiate(aiObject);
            teamH[i].transform.position = Settings.startPositionHome[i];
            teamH[0].GetComponent<Player>().AddTeammate(teamH[i], i - 1);
        }

        foreach (var ob in teamH)
        {
            ob.tag = "HomePlayers";
            var t = ob.transform.GetChild(2).GetChild(0);
            t.GetComponent<SpriteRenderer>().sprite = atlasForm.GetSprite("HockeyTeamNHL_" + homeColor);
        }


        logoHome.sprite = atlasForm.GetSprite("HockeyTeamNHL_" + homeColor);
        logoAway.sprite = atlasForm.GetSprite("HockeyTeamNHL_" + awayColor);

        countdown = periodMinute * 60;
        timeText.text = "20:00";
        period.text = periodNow + " Период";
    }

    void UpdateTimeText()
    {
        int minute = Mathf.FloorToInt(countdown / 60f);
        int second = Mathf.FloorToInt(countdown % 60);
        timeText.text = minute + ":" + second;
        period.text = periodNow + " Период";
    }

    // Update is called once per frame
    void Update()
    {
        if (periodNow <= 3)
        {
            countdown -= Time.deltaTime * periodMinute / speedPerionInMinute;
            UpdateTimeText();
            if (countdown <= 0)
            {
                periodNow++;
                if (periodNow > 3)
                {
                    EndGame();
                }
                countdown = periodMinute * 60;
                ResetPosition();
            }
        }
    }

    void EndGame()
    {
        if (Int32.Parse(textGoalH.text) > Int32.Parse(textGoalA.text))
        {
            endGameWhoWin.text = "Победа!";
        }
        else
        {
            endGameWhoWin.text = "Ты проиграл(";
        }
        endGameScore.text = textGoalH.text + ":" + textGoalA.text;
        endGamePanel.SetActive(true);
    }

    public void ReturnToMainScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    void ResetPosition()
    {
        if (periodNow % 2 == 0)
        {
            for (int i = 0; i < Settings.countPlayersInTeam; i++)
            {
                teamA[i].transform.position = Settings.startPositionHome[i];
                teamH[i].transform.position = Settings.startPositionAway[i];
            }
        }
        else
        {
            for (int i = 0; i < Settings.countPlayersInTeam; i++)
            {
                teamA[i].transform.position = Settings.startPositionAway[i];
                teamH[i].transform.position = Settings.startPositionHome[i];
            }
        }
        Pick.Instance.Reset();
    }

}
