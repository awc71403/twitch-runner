using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class SpriteCreator : MonoBehaviour
{
    public Sprite[] spriteModel;

    //Random X from -1.8 to 3.8
    internal void CreateSprite(GameObject playerObject, string sprite, string color)
    {

        playerObject.name = playerObject.GetComponent<Player>().Username;
        GameObject outline = GameObject.Find("Outline");

        if (color == "red")
        {
            playerObject.GetComponent<Outline>().color = 0;
        }
        else if (color == "green")
        {
            playerObject.GetComponent<Outline>().color = 1;
        }
        else if (color == "blue")
        {
            playerObject.GetComponent<Outline>().color = 2;
        }
        else if (color == "black")
        {
            playerObject.GetComponent<Outline>().color = 3;
        }
        else if (color == "white")
        {
            playerObject.GetComponent<Outline>().color = 4;
        }

        if (sprite == "velkoz")
        {
            playerObject.GetComponent<SpriteRenderer>().sprite = spriteModel[0];
        }
        else if (sprite == "probe")
        {
            playerObject.GetComponent<SpriteRenderer>().sprite = spriteModel[1];
        }
        else if (sprite == "chao")
        {
            playerObject.GetComponent<SpriteRenderer>().sprite = spriteModel[2];
        }
        else if (sprite == "charmander")
        {
            playerObject.GetComponent<SpriteRenderer>().sprite = spriteModel[3];
        }
        else if (sprite == "link")
        {
            playerObject.GetComponent<SpriteRenderer>().sprite = spriteModel[4];
        }
        else
        {
            playerObject.GetComponent<SpriteRenderer>().sprite = spriteModel[5];
            playerObject.GetComponent<Outline>().eraseRenderer = true;
        }

        Vector3 newPos = playerObject.transform.position;
        newPos.x = UnityEngine.Random.Range(-1.8f, 3.8f);
        playerObject.transform.position = newPos;
        Debug.Log(string.Format("Created {0}", sprite));
    }

    internal void LeaderboardSprite(GameObject playerObject, string sprite, string color)
    {
        Debug.Log("Test LeaderboardSprite");
        if (color == "red")
        {
            playerObject.GetComponent<Outline>().color = 0;
        }
        else if (color == "green")
        {
            playerObject.GetComponent<Outline>().color = 1;
        }
        else if (color == "blue")
        {
            playerObject.GetComponent<Outline>().color = 2;
        }
        else if (color == "black")
        {
            playerObject.GetComponent<Outline>().color = 3;
        }
        else if (color == "white")
        {
            playerObject.GetComponent<Outline>().color = 4;
        }

        if (sprite == "velkoz")
        {
            playerObject.GetComponent<SpriteRenderer>().sprite = spriteModel[0];
        }
        else if (sprite == "probe")
        {
            playerObject.GetComponent<SpriteRenderer>().sprite = spriteModel[1];
        }
        else if (sprite == "chao")
        {
            playerObject.GetComponent<SpriteRenderer>().sprite = spriteModel[2];
        }
        else if (sprite == "charmander")
        {
            playerObject.GetComponent<SpriteRenderer>().sprite = spriteModel[3];
        }
        else if (sprite == "link")
        {
            playerObject.GetComponent<SpriteRenderer>().sprite = spriteModel[4];
        }
        else
        {
            playerObject.GetComponent<SpriteRenderer>().sprite = spriteModel[5];
            playerObject.GetComponent<Outline>().eraseRenderer = true;
        }
    }
}
