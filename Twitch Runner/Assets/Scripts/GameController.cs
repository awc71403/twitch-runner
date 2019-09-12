using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public enum GameState
    {
        Join,
        Deciding,
        Number,
        Running,
        Winner
    }

    public GameState CurrentGameState { get; private set; }

    [SerializeField]
    private float joinDuration = 30;
    [SerializeField]
    private float decidingDuration = 20;
    [SerializeField]
    private float numberDuration = 2;
    [SerializeField]
    private float runningDuration = 0;
    [SerializeField]
    private TextMeshProUGUI countdownText;
    [SerializeField]
    private TextMeshProUGUI blockerText;
    [SerializeField]
    private TextMeshProUGUI joinText;
    [SerializeField]
    private TextMeshProUGUI colorText;
    [SerializeField]
    private TextMeshProUGUI spriteText;
    [SerializeField]
    private TextMeshProUGUI winText;
    [SerializeField]
    private TextMeshProUGUI leaderboardText;
    [SerializeField]
    private TextMeshProUGUI leaderboardTitle;
    [SerializeField]
    private Text pauseText;

    private float timeRemainingInCurrentState;
    private int blockernumber;
    private int pauseTimer = 1;
    private int bots = 0;
    private RunController runController;
    private PlayerController playerController;

    private void Start()
    {
        bots = PlayerPrefs.GetInt("Bots");
        SetState(GameState.Join);
        runController = FindObjectOfType<RunController>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void SetState(GameState state)
    {
        CurrentGameState = state;
        switch (CurrentGameState)
        {
            case GameState.Join:
                timeRemainingInCurrentState = joinDuration;
                break;
            case GameState.Deciding:
                blockerText.SetText("");
                List<Player> finished = playerController.GetFinished();
                if (finished != null)
                {
                    Win(finished);
                    break;
                }
                for (int i = 0; i < bots; i++)
                {
                    runController.SetRun($"Bot{i}", Random.Range(1, 9));
                }
                timeRemainingInCurrentState = decidingDuration;
                break;
            case GameState.Number:
                timeRemainingInCurrentState = numberDuration;
                blockernumber = Random.Range(1, 9);
                blockerText.SetText(blockernumber.ToString());
                break;
            case GameState.Running:
                timeRemainingInCurrentState = runningDuration;
                runController.Run(blockernumber, timeRemainingInCurrentState);
                break;
            case GameState.Winner:
                break;
        }
    }

    private void Update()
    {
        if (pauseTimer == 1)
        {
            if (CurrentGameState != GameState.Winner)
            {
                timeRemainingInCurrentState -= Time.deltaTime;
                if (CurrentGameState != GameState.Number && CurrentGameState != GameState.Running)
                {
                    countdownText.SetText(timeRemainingInCurrentState.ToString("N0"));
                }
                if (timeRemainingInCurrentState <= 0)
                {
                    MoveToNextState();
                }
            }
        }
    }

    private void MoveToNextState()
    {
        switch (CurrentGameState)
        {
            case GameState.Join:
                joinText.gameObject.SetActive(false);
                colorText.gameObject.SetActive(false);
                spriteText.gameObject.SetActive(false);
                leaderboardTitle.gameObject.SetActive(true);
                SetState(GameState.Deciding);
                break;
            case GameState.Deciding:
                SetState(GameState.Number);
                break;
            case GameState.Number:
                SetState(GameState.Running);
                break;
            case GameState.Running:
                SetState(GameState.Deciding);
                break;
        }
    }

    public void Win(List<Player> finished)
    {
        Player furthest = new Player() { Username = "Default", Distance = 0 };
        List<Player> winner = new List<Player>();
        foreach (var player in finished)
        {
            if (player.Distance > furthest.Distance)
            {
                winner.Clear();
                furthest = player;
                winner.Add(player);
            }
            else if (player.Distance == furthest.Distance)
            {
                winner.Add(player);
            }
        }
        if (winner.Count == 1)
        {
            winText.text = $"{furthest.Username} won!";
        }
        else if (winner.Count == 2)
        {
            winText.text = $"{winner[0].Username} and {winner[1].Username} won!";
        }
        else
        {
            string winUsers = "";
            for (int i = 0; i < winner.Count - 1; i++)
            {
                winUsers += $"{winner[i].Username}, ";
            }
            winText.text = winUsers + $"and {winner[winner.Count - 1].Username} won!";
        }
        Debug.Log(string.Format("{0} won!", furthest.Username));
        SetState(GameState.Winner);
    }

    public void Pause()
    {
        if (pauseTimer == 1)
        {
            pauseTimer = 2;
            pauseText.text = "Resume";
        }
        else
        {
            pauseTimer = 1;
            pauseText.text = "Pause";
        }
    }
}

