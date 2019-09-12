using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private List<Player> players = new List<Player>();
    private List<Player> finished = new List<Player>();

    public GameObject playerPrefab;

    [SerializeField]
    RunController runController;
    [SerializeField]
    LeaderboardController leaderboardController;

    private SpriteCreator spriteCreator;
    private int bots = 0;

    private string[] colors = { "red", "blue", "green", "black", "white" };
    private List<string> listofcolors;

    private string[] sprites = { "velkoz", "probe", "chao", "charmander", "link", "magnemite"};
    private List<string> listofsprites;

    private void Start()
    {
        bots = PlayerPrefs.GetInt("Bots");
        spriteCreator = FindObjectOfType<SpriteCreator>();
        listofcolors = colors.ToList<string>();
        listofsprites = sprites.ToList<string>();
        for (int i = 0; i < bots; i++)
        {
            CreatePlayer($"Bot{i}", "black", "magnemite");
        }
    }

    internal Player GetPlayer(string username)
    {
        var player = players.FirstOrDefault(t => t.Username == username);
        return player;
    }

    internal void CreatePlayer(string username, string color, string sprite)
    {
        if (listofcolors.Contains(color) && listofsprites.Contains(sprite))
        {
            var player = players.FirstOrDefault(t => t.Username == username);
            if (player == null)
            {
                GameObject playerObject = Instantiate(playerPrefab);
                playerObject.GetComponent<Player>().Username = username;
                playerObject.GetComponent<Player>().Distance = 0;
                playerObject.GetComponent<Player>().Color = color;
                playerObject.GetComponent<Player>().Sprite = sprite;
                playerObject.GetComponentInChildren<Canvas>().gameObject.GetComponentInChildren<TextMeshProUGUI>().text = username;
                spriteCreator.CreateSprite(playerObject, sprite, color);
                player = playerObject.GetComponent<Player>();
                players.Add(player);
                leaderboardController.AddLeaderboard(player);
                runController.SetRun(username, 0);
                Debug.Log(string.Format("{0} joined as {1} {2}.", username, color, sprite));
            }
        }
        else
        {
            Debug.Log(string.Format("Color {0} or sprite {1} does not exist", color, sprite));
        }
    }

    internal void AddFinished(Player player)
    {
        finished.Add(player);
    }

    internal List<Player> GetFinished()
    {
        if (finished.Count == 0)
        {
            return null;
        }
        else
        {
            return finished;
        }
    }
}