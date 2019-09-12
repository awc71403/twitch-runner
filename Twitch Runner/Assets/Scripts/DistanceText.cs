using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DistanceText : MonoBehaviour
{
    TextMeshProUGUI text;
    private List<string> runMessages = new List<string>();

    [SerializeField]
    private int maxMessages = 3;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        FindObjectOfType<RunController>().OnRunMade += DistanceText_OnRunMade;
    }

    private void DistanceText_OnRunMade(Player player, int distanceRunning)
    {
        runMessages.Add(string.Format("{0} is running {1}.", player.Username, distanceRunning));

        if (runMessages.Count > maxMessages)
        {
            runMessages.RemoveAt(0);
        }

        StringBuilder runString = new StringBuilder();
        foreach (var runMessage in runMessages)
        {
            runString.AppendLine(runMessage);
        }
        text.SetText(runString);
    }
}
