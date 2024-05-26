using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public List<Transform> leftPoints, rightPoints,turnPoints;
    public GameManagers gameManagers;
    Transform target;
    public bool egg = false, milk = false, turns=false;
    public int pointCount = 0;
    int eggPrice=5, milkPrice = 5;

    void Start()
    {
        if (UnityEngine.Random.Range(0, 100) % 2 == 0)
        {
            egg = true;
            milk = false;
        }
        else
        {
            egg = false;
            milk = true;
        }
    }
    void Buy(string key)
    {
        if (key=="E")
        {
            int egg = UnityEngine.Random.Range(0, int.Parse(math.floor(PlayerPrefs.GetInt("EggCount") * 0.15f).ToString()));
            if(egg>1)
            {
                PlayerPrefs.SetInt("EggCount", PlayerPrefs.GetInt("EggCount")-egg);
                PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount")+(egg*eggPrice));
                Debug.Log("Merhabalar," + egg + " tane yumurta aldým. Ödeme tamamlandý.");
            }else if(PlayerPrefs.GetInt("EggCount")>0)
            {
                PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount") + (PlayerPrefs.GetInt("EggCount") * eggPrice));
                PlayerPrefs.SetInt("EggCount", 0);

            }
            else
            {
                Debug.Log("Merhabalar, yumurtanýz kalmamýþ. Bu nasýl bakkal?");
            }
           
        }
        else
        {
            int milk = UnityEngine.Random.Range(0, int.Parse(math.floor(PlayerPrefs.GetInt("MilkCount") * 0.15f).ToString()));
            if (milk > 1)
            {
                PlayerPrefs.SetInt("MilkCount", PlayerPrefs.GetInt("MilkCount") - milk);
                PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount") + (milk * milkPrice));
                Debug.Log("Merhabalar," + milk + " tane süt aldým. Ödeme tamamlandý.");
            }
            else if (PlayerPrefs.GetInt("MilkCount") > 0)
            {
                PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount") + (PlayerPrefs.GetInt("MilkCount") * milk));
                PlayerPrefs.SetInt("MilkCount", 0);

            }
            else
            {
                Debug.Log("Merhabalar, sütünüz kalmamýþ. Bu nasýl bakkal?");
            }
        }

        gameManagers.LoadPlayerData();
        Invoke("TurnBack", 1f);
    }
    void TurnBack()
    {
        turns = true;
    }
    void Update()
    {
        if (milk)
        {
            Transform t = rightPoints[pointCount];
            transform.position = Vector3.MoveTowards(transform.position, t.position, 2f * Time.deltaTime);
            if (transform.position == t.position)
            {
                pointCount++;
                if (pointCount == rightPoints.Count)
                {
                    milk = false;
                    pointCount = 0;
                    Buy("M");
                }
                

            }
        }
        if (egg)
        {
            Transform t = leftPoints[pointCount];
            transform.position = Vector3.MoveTowards(transform.position, t.position, 2f * Time.deltaTime);
            if (transform.position == t.position)
            {
                pointCount++;
                if (pointCount ==leftPoints.Count)
                {
                    egg = false;
                    pointCount = 0;
                    Buy("E");
                    
                }

            }
        }
        if (turns)
        {
            Transform t = turnPoints[pointCount];
            transform.position = Vector3.MoveTowards(transform.position, t.position, 2f * Time.deltaTime);
            if (transform.position == t.position)
            {
                pointCount++;
                if (pointCount == turnPoints.Count)
                {
                    turns = false;
                    pointCount = 0;
                }

            }
        }
    }
}