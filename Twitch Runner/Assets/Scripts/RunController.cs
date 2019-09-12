using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunController : MonoBehaviour
{
    [SerializeField]
    LeaderboardController leaderboardController;
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    TextMeshProUGUI goalDistanceText;

    public event Action<Player, int> OnRunMade = delegate { };
    public event Action<Player> Leaderboard = delegate { };

    private Dictionary<Player, int> rundistance = new Dictionary<Player, int>();
    private int finishDistance = 40;

    private void Start()
    {
        finishDistance = PlayerPrefs.GetInt("Distance");
        goalDistanceText.text = $"Goal: {finishDistance}";
    }

    public void SetRun(string username, int numberDistanceToRun)
    {
        Player player = playerController.GetPlayer(username);

        if (player != null)
        {
            if (rundistance.ContainsKey(player))
            {
                rundistance.Remove(player);
            }
            rundistance.Add(player, numberDistanceToRun);
            OnRunMade(player, numberDistanceToRun);
        }


    }

    internal void Run(int blocker, float time)
    {
        Debug.Log(string.Format("Blocker is {0}", blocker));
        foreach (var playerpair in rundistance)
        {
            if (playerpair.Value <= blocker)
            {
                playerpair.Key.Distance += playerpair.Value;
                playerpair.Key.Move(playerpair.Value, finishDistance);
                Debug.Log(string.Format("{0} ran {1}.", playerpair.Key.Username, playerpair.Value));
                if (playerpair.Key.Distance >= finishDistance)
                {
                    playerController.AddFinished(playerpair.Key);
                }
            }
            else
            {
                Debug.Log(string.Format("{0} was blocked.", playerpair.Key.Username));
            }
        }
        leaderboardController.CheckLeaderboard();
    }
}
