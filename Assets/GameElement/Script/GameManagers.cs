using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    public GameObject orderPanel,prefabCustomer,cameras;
    public List<Transform> leftPoints, rightPoints, turnPoints;
    public Transform stand;
    public TextMeshProUGUI txtEgg, txtMilk;

    public TextMeshProUGUI lblEgg, lblMilk, lblCoin;

    int coin = 0;
    void Start()
    {
        LoadPlayerData();
        InvokeRepeating("CustomerCreate",3f,5f);

    }
   
    public void GotoVillage(string direction)// r or l
    {

        Vector3 vector=new Vector3(cameras.transform.position.x, cameras.transform.position.y,-10f);
        vector.x += direction == "r" ? 17.7f : -17.7f;
        if (Mathf.Floor(vector.x) <= 18f && Mathf.Floor(vector.x) >= -18f)
        {
            cameras.GetComponent<CameraSc>().CameraMove(vector);
        }
        
    }
    
    public void LoadPlayerData()
    {
        lblCoin.text = PlayerPrefs.GetInt("CoinCount") != 0 ? PlayerPrefs.GetInt("CoinCount").ToString() : "0" ;
        lblEgg.text = PlayerPrefs.GetInt("EggCount") != 0 ? PlayerPrefs.GetInt("EggCount").ToString() : "0";
        lblMilk.text = PlayerPrefs.GetInt("MilkCount") != 0 ? PlayerPrefs.GetInt("MilkCount").ToString() : "0";
        
    }
    void CustomerCreate()
    {
       GameObject obj = Instantiate(prefabCustomer, new Vector2(0f, -5f), Quaternion.identity);
        obj.GetComponent<Customer>().leftPoints = leftPoints;
        obj.GetComponent<Customer>().rightPoints = rightPoints;
        obj.GetComponent<Customer>().turnPoints = turnPoints;
        obj.GetComponent<Customer>().gameManagers = GetComponent<GameManagers>();
       
        
    }

 
}
