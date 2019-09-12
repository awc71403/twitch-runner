using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    private int inleaderboard = 0;
    private const int maxLeaderboard = 5;
    private List<Player> leaderboard = new List<Player>();
    private List<string> runMessages = new List<string>();
    private List<string> placementPrefix = new List<string>();
    private List<GameObject> leaderboardIcons = new List<GameObject>();

    [SerializeField]
    SpriteCreator spriteCreator;
    [SerializeField]
    TextMeshProUGUI leaderboardText;
    [SerializeField]
    GameObject first;
    [SerializeField]
    GameObject second;
    [SerializeField]
    GameObject third;
    [SerializeField]
    GameObject fourth;
    [SerializeField]
    GameObject fifth;

    private void Start()
    {
        leaderboardIcons.Add(first);
        leaderboardIcons.Add(second);
        leaderboardIcons.Add(third);
        leaderboardIcons.Add(fourth);
        leaderboardIcons.Add(fifth);

        for (int i = 0; i < maxLeaderboard; i++)
        {
            int number = i + 1;
            if (number % 10 == 1)
            {
                placementPrefix.Add(string.Format("{0}st:", number));
            }
            else if (number % 10 == 2)
            {
                placementPrefix.Add(string.Format("{0}nd:", number));
            }
            else if (number % 10 == 3)
            {
                placementPrefix.Add(string.Format("{0}rd:", number));
            }
            else
            {
                placementPrefix.Add(string.Format("{0}th:", number));
            }
        }
    }

    public void CheckLeaderboard()
    {
        leaderboard.Sort(ComparePlayerDistance);

        for (int i = 0; i < inleaderboard; i++)
        {
            if (leaderboard[i] == null)
            {
                break;
            }
            spriteCreator.LeaderboardSprite(leaderboardIcons[i], leaderboard[i].Sprite, leaderboard[i].Color);
            runMessages.Add(string.Format("{0}\n{1} {2}", placementPrefix[i], leaderboard[i].Username, leaderboard[i].Distance));
        }

        StringBuilder runString = new StringBuilder();
        foreach (var runMessage in runMessages)
        {
            runString.AppendLine(runMessage);
        }
        leaderboardText.SetText(runString);
        runMessages.Clear();
    }

    public void AddLeaderboard(Player player)
    {
        leaderboard.Add(player);
        if (inleaderboard < maxLeaderboard)
        {
            inleaderboard++;
        }
    }

    private static int ComparePlayerDistance(Player x, Player y)
    {
        if (x == null)
        {
            if (y == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            if (y == null)
            {
                return -1;
            }
            else
            {
                if (x.Distance > y.Distance)
                {
                    return -1;
                }
                else if (y.Distance > x.Distance)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
