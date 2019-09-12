using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPref : MonoBehaviour
{
    [SerializeField]
    private InputField OAuth;
    [SerializeField]
    private InputField Nickname;
    [SerializeField]
    private InputField Channel;
    [SerializeField]
    private InputField Distance;
    [SerializeField]
    private InputField Bots;
    [SerializeField]
    private Button Gamemode;
    [SerializeField]
    private Text ChannelOne;
    [SerializeField]
    private Text ChannelTwo;
    [SerializeField]
    private Text ChannelThree;
    [SerializeField]
    private Text ChannelFour;
    [SerializeField]
    private Text TwitchText;
    [SerializeField]
    private Text GamemodeText;

    private int VvVorSvS = 1;
    private int OpenorHide = 1;
    private int distanceInt = 40;
    private int botsInt = 0;


    private void Start()
    {
        PlayerPrefs.SetString("ChannelOne", ChannelOne.text);
        PlayerPrefs.SetString("ChannelTwo", ChannelTwo.text);
        PlayerPrefs.SetString("ChannelThree", ChannelThree.text);
        PlayerPrefs.SetString("ChannelFour", ChannelFour.text);

        try
        {
            OAuth.text = $"{PlayerPrefs.GetString("OAuth")}";
            Nickname.text = $"{PlayerPrefs.GetString("Nickname")}";
            Channel.text = $"{PlayerPrefs.GetString("Channel")}";
        }
        catch (NullReferenceException)
        {
            Debug.Log("No OAuth, Nickname, or Channel saved.");
        }
    }

    public void StartButton()
    {
        PlayerPrefs.SetString("OAuth", OAuth.text);
        Debug.Log("SetOAuth");
        PlayerPrefs.SetString("Nickname", Nickname.text);
        Debug.Log("SetNickname");
        PlayerPrefs.SetString("Channel", Channel.text);
        Debug.Log("SetChannel");
        int.TryParse(Distance.text, out distanceInt);
        PlayerPrefs.SetInt("Distance", distanceInt);
        int.TryParse(Bots.text, out botsInt);
        PlayerPrefs.SetInt("Bots", botsInt);
        if (ChannelOne.text != "")
        {
            PlayerPrefs.SetString("ChannelOne", ChannelOne.text);
            Debug.Log("SetChannelOne");
        }
        if (ChannelTwo.text != "")
        {
            PlayerPrefs.SetString("ChannelTwo", ChannelTwo.text);
            Debug.Log("SetChannelTwo");
        }
        if (ChannelThree.text != "")
        {
            PlayerPrefs.SetString("ChannelThree", ChannelThree.text);
            Debug.Log("SetChannelThree");
        }
        if (ChannelFour.text != "")
        {
            PlayerPrefs.SetString("ChannelFour", ChannelFour.text);
            Debug.Log("SetChannelFour");
        }
        SceneManager.LoadScene(1);
    }

    public void TwitchButton()
    {
        if (OpenorHide == 1)
        {
            OAuth.gameObject.SetActive(true);
            Nickname.gameObject.SetActive(true);
            Channel.gameObject.SetActive(true);
            Distance.gameObject.SetActive(false);
            Bots.gameObject.SetActive(false);
            TwitchText.text = "Hide Settings";
            OpenorHide = 2;
        }
        else
        {
            OAuth.gameObject.SetActive(false);
            Nickname.gameObject.SetActive(false);
            Channel.gameObject.SetActive(false);
            Distance.gameObject.SetActive(true);
            Bots.gameObject.SetActive(true);
            TwitchText.text = "Twitch Settings";
            OpenorHide = 1;
        }
    }

    public void ToVvVorSvS()
    {
        if (VvVorSvS == 1)
        {
            VvVorSvS = 2;
            GamemodeText.text = "Streamer vs Streamer";
            PlayerPrefs.SetInt("VvVorSvS", VvVorSvS);
        }
        else
        {
            VvVorSvS = 1;
            GamemodeText.text = "Viewer vs Viewer";
            PlayerPrefs.SetInt("VvVorSvS", VvVorSvS);
        }
    }
}
