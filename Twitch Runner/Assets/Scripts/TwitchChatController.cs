using System;
using UnityEngine;

public class TwitchChatController : MonoBehaviour
{
    private GameController gameController;
    private RunController runController;
    private SpriteCreator spriteCreator;
    private PlayerController playerController;

    //:wojteknician!wojteknician@wojteknician.tmi.twitch.tv PRIVMSG #wojteknician :test
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        runController = FindObjectOfType<RunController>();
        playerController = FindObjectOfType<PlayerController>();
        spriteCreator = FindObjectOfType<SpriteCreator>();
        FindObjectOfType<TwitchIRC>().messageRecievedEvent.AddListener(HandleMessageReceived);
    }

    private void HandleMessageReceived(string text)
    {
        int nameStart = 1;
        int nameEnd = text.IndexOf('!');
        string username = text.Substring(nameStart, nameEnd - nameStart);

        int messageStart = text.IndexOf(':', 10) + 1;
        string message = text.Substring(messageStart, text.Length - messageStart);

        string[] words = message.Split(' ');
        string join = words[0];

        Debug.Log(string.Format("{0}: {1}", username, message));

        int numberDistanceToRun;

        if (join.Equals("!join"))
        {
            if (words.Length == 3)
            {
                string color = words[1];
                string sprite = words[2];
                if (gameController.CurrentGameState == GameController.GameState.Join)
                {
                    playerController.CreatePlayer(username, color, sprite);
                }
                else
                {
                    Debug.Log(string.Format("{0} can not join.", username));
                }
            }
            else
            {
                Debug.Log(string.Format("Bad join command by {0}", username));
            }
        }

        if (gameController.CurrentGameState == GameController.GameState.Deciding || gameController.CurrentGameState == GameController.GameState.Number || gameController.CurrentGameState == GameController.GameState.Running)
        {
            if (int.TryParse(message, out numberDistanceToRun))
            {
                if (numberDistanceToRun > 0 && numberDistanceToRun < 10)
                {
                    runController.SetRun(username, numberDistanceToRun);
                }
            }
        }
        else
        {
            if (!(gameController.CurrentGameState == GameController.GameState.Join))
            {
                Debug.Log(string.Format("{0} can not decide.", username));
            }
        }
    }
}
