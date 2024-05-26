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
    public GameObject gmPlayer;
    Transform target;
    public bool egg = false, milk = false, turns=false;
    public int pointCount = 0;
    int eggPrice=5, milkPrice = 5;
    Animation idle, run;
    public GameObject panel;
    public TextMeshProUGUI txtMessage;

    void Start()
    {
       
        if (UnityEngine.Random.Range(0, 100) % 2 == 0)
        {
            egg = true;
            milk = false;
            gmPlayer.gameObject.transform.Rotate(0f, 180f, 0f);

        }
        else
        {
            egg = false;
            milk = true;
            gmPlayer.gameObject.transform.Rotate(0f, 0f, 0f);
        }
    }
    void Buy(string key)
    {
        gameObject.GetComponent<Animator>().SetBool("isRun", true);
        if (key=="E")
        {
            int egg = UnityEngine.Random.Range(0, int.Parse(math.floor(PlayerPrefs.GetInt("EggCount") * 0.015f).ToString()));
            if(egg>1)
            {
                PlayerPrefs.SetInt("EggCount", PlayerPrefs.GetInt("EggCount")-egg);
                PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount")+(egg*eggPrice));
                txtMessage.text = "+Merhabalar," + egg + " tane yumurta aldým. \n-Ödeme tamamlandý.Ýyi günler :)";
            }
            else if(PlayerPrefs.GetInt("EggCount")>0)
            {
                int a = UnityEngine.Random.Range(0, PlayerPrefs.GetInt("EggCount"));
                txtMessage.text = "+Merhabalar," + a + " tane yumurta aldým.\n-Ödeme tamamlandý.Ýyi günler :)";
                PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount") + (a * eggPrice));
                PlayerPrefs.SetInt("EggCount", PlayerPrefs.GetInt("EggCount")-a);
            }
            else
            {
                txtMessage.text = "Merhabalar, yumurtanýz kalmamýþ. Bu nasýl dükkan?";
            }
            gmPlayer.gameObject.transform.Rotate(0f, 180f, 0f);

        }
        else
        {
            int milk = UnityEngine.Random.Range(0, int.Parse(math.floor(PlayerPrefs.GetInt("MilkCount") * 0.015f).ToString()));
            if (milk > 1)
            {
                PlayerPrefs.SetInt("MilkCount", PlayerPrefs.GetInt("MilkCount") - milk);
                PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount") + (milk * milkPrice));
                txtMessage.text = "+Merhabalar," + milk + " tane süt aldým.\n-Ödeme tamamlandý.Ýyi günler :)";
            }
            else if (PlayerPrefs.GetInt("MilkCount") > 0)
            {
                int a = UnityEngine.Random.Range(0, PlayerPrefs.GetInt("MilkCount"));
                txtMessage.text = "+Merhabalar," + a + " tane süt aldým.\n-Ödeme tamamlandý.Ýyi günler :)";
                PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount") + (a * milkPrice));
                PlayerPrefs.SetInt("MilkCount", PlayerPrefs.GetInt("MilkCount")-a);

            }
            else
            {
                txtMessage.text  = "Merhabalar, sütünüz kalmamýþ. Bu nasýl dükkan?";
            }
            gmPlayer.gameObject.transform.Rotate(0f, 180f, 0f);

        }

        gameManagers.LoadPlayerData();
        panel.gameObject.SetActive(true);
        Invoke("TurnBack", 3f);
    }
    void TurnBack()
    {
        gameObject.GetComponent<Animator>().SetBool("isRun", false);
        
        turns = true;
        panel.gameObject.SetActive(false);
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