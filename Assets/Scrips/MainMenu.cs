using Assets.Scrips;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] Button home;
    [SerializeField] Button away;

    [SerializeField] 
    private SpriteAtlas atlasForm;

    [SerializeField]
    private GameObject leftHome;
    [SerializeField]
    private GameObject rightHome;
    [SerializeField]
    private GameObject leftAway;
    [SerializeField]
    private GameObject rightAway;
    [SerializeField]
    private Text countPlayer;
    [SerializeField]
    private Text speedSetting;
    [SerializeField]
    private Button leftCountPlayer;
    [SerializeField]
    private Button rightCountPlayer;
    [SerializeField]
    private Button leftSpeed;
    [SerializeField]
    private Button rightSpeed;

    private int[] speeds = new int[] { 1, 5, 10, 20 }; 

    private void Start()
    {
        home.image.sprite = atlasForm.GetSprite("HockeyTeamNHL_" + Settings.homeSkin);
        away.image.sprite = atlasForm.GetSprite("HockeyTeamNHL_" + Settings.awaySkin);
        countPlayer.text = Settings.countPlayersInTeam.ToString();
        speedSetting.text = Settings.speed.ToString();
    }

    private void ChangePlayer(int add, int limit, Button isSelect, Button another)
    {
        if (Settings.countPlayersInTeam == limit) return;
        Settings.countPlayersInTeam+=add;
        countPlayer.text = Settings.countPlayersInTeam.ToString();
        if (Settings.countPlayersInTeam == limit)
        {
            isSelect.interactable = false;
        }
        another.interactable = true;
    }


    public void AddPlayer() => ChangePlayer(1, 6, rightCountPlayer, leftCountPlayer);
    public void RemovePlayer() => ChangePlayer(-1, 1, leftCountPlayer, rightCountPlayer);
    
    private void ChangeSpeed(int add, int limit, Button isSelect, Button another)
    {
        if (Settings.speed == limit) return;
        another.interactable = true;
        int index = -1;
        for (int i = 0; i < speeds.Length; i++)
        {
            if (speeds[i] == Settings.speed)
            {
                index = i;
                break;
            }
        }
        if (index == -1)
        {
            Settings.speed = speeds[0];
            leftSpeed.interactable = false;
            rightSpeed.interactable = true;
        }
        else
        {
            Settings.speed = speeds[index + add];
            if (index + add == limit)
            {
                isSelect.interactable = false;
            }
        }
        speedSetting.text = Settings.speed.ToString();
    }

    public void ChangeSpeedPlus() => ChangeSpeed(1, 3, rightSpeed, leftSpeed);
    public void ChangeSpeedMinus() => ChangeSpeed(-1, 0, leftSpeed, rightSpeed);


    private void SelectAway(int add, GameObject select, GameObject another, int limit)
    {
        Settings.awaySkin += add;
        if (Settings.awaySkin == Settings.homeSkin)
        {
            Settings.awaySkin += add;
        }
        Settings.awaySkin = (Settings.awaySkin + atlasForm.spriteCount) % atlasForm.spriteCount;
        away.image.sprite = atlasForm.GetSprite("HockeyTeamNHL_" + Settings.awaySkin);
    }

    private void SelectHome(int add, GameObject select, GameObject another, int limit)
    {
        Settings.homeSkin += add;
        if (Settings.awaySkin == Settings.homeSkin)
        {
            Settings.homeSkin += add;
        }
        Settings.homeSkin = (Settings.homeSkin + atlasForm.spriteCount) % atlasForm.spriteCount;
        home.image.sprite = atlasForm.GetSprite("HockeyTeamNHL_" + Settings.homeSkin);
    }



    public void SelectLeftHome() => SelectHome(-1, leftHome, rightHome, 0);
    public void SelectRightHome() => SelectHome(1, rightHome, leftHome, atlasForm.spriteCount - 1);

    public void SelectLeftAway() => SelectAway(-1, leftAway, rightAway, 0);
    public void SelectRightAway() => SelectAway(1, rightAway, leftAway, atlasForm.spriteCount - 1);


    public void OnClickStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
