using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Username;
    public int Distance;
    public string Color;
    public string Sprite;

    float ElapsedTime;
    float FinishTime;
    Vector3 StartPosition;
    Vector3 Target;
    private int switchNumber = 0;

    void Start()
    {
        StartPosition = gameObject.transform.position;
        Target = gameObject.transform.position;
        ElapsedTime = 0;
        FinishTime = 5f;
    }

    void Update()
    {
        if (switchNumber == 1)
        {
            if (ElapsedTime >= FinishTime)
            {
                switchNumber = 0;
                ElapsedTime = 0;
                StartPosition = Target;
                return;
            }
            ElapsedTime += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(StartPosition, Target, ElapsedTime / FinishTime);
        }
    }

    internal void Move(int distancetomove, int finishDistance)
    {
        Debug.Log($"{Username} is moving");
        Target.z -= (float) distancetomove * (float) 25 / (float) finishDistance;
        if (Target.z < -25)
        {
            Target.z = -25;
        }
        switchNumber = 1;
    }
}
