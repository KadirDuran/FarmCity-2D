using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CropManager : MonoBehaviour
{

    public GameObject cropPanel,item=null,chickenParent,sheepParent;
    public Image image;

    public TextMeshProUGUI txtCropCount,txtUpgradePrice;
    public Sprite spEgg, spMilk;

    string types = "";
    public List<GameObject> chicken,sheeps;
    
    public void PanelInfo(string CropCount,string UpgradePrice,string type)
    {
        ChangePanelStatu();
        txtCropCount.text = "X "+ CropCount;
        txtUpgradePrice.text ="Upgrade : "+ UpgradePrice;
        image.sprite = type == "C" ? spEgg : spMilk;
        types = type;
    }
    public void ChangePanelStatu() {
       
        cropPanel.SetActive(!cropPanel.activeSelf);
      
    }
      void LoadSheep(string types)
    {
        for (int i = 0; i < 9; i++) {
     
            if (types == "S")
            {
                int lvl = PlayerPrefs.GetInt("Sheep" + i.ToString());
                GameObject obj = Instantiate(sheeps[lvl], new Vector3(UnityEngine.Random.Range(9.5f, 25.5f), UnityEngine.Random.Range(-4.5f, 4.5f), 0f), Quaternion.identity);
                obj.GetComponent<Sheep>().cropManager = gameObject.GetComponent<CropManager>();
                obj.name="Sheep"+i.ToString();
                obj.transform.parent = sheepParent.transform;
            }
            else
            {
                int lvl = PlayerPrefs.GetInt("Chicken" + i.ToString());
                GameObject obj = Instantiate(chicken[lvl], new Vector3(UnityEngine.Random.Range(-25.5f,-9.5f) , UnityEngine.Random.Range(-4.5f, 4.5f), 0f), Quaternion.identity);
                obj.GetComponent<Chicken>().cropManager = gameObject.GetComponent<CropManager>();
                obj.name = "Chicken" + i.ToString();
                obj.transform.parent = chickenParent.transform;

            }


        }



    }
    public void GetCrop()
    {
        if (int.Parse(txtCropCount.text.Replace("X ", "")) > 0)
        {
            if(types == "C")
            {
                PlayerPrefs.SetInt("EggCount", PlayerPrefs.GetInt("EggCount") >= 0 ? PlayerPrefs.GetInt("EggCount") + int.Parse(txtCropCount.text.Replace("X ", "")) : 0);
                item.GetComponent<Chicken>().eggCount = 0;
                txtCropCount.text = "X 0";
                gameObject.GetComponent<GameManagers>().LoadPlayerData();
                item.GetComponent<Chicken>().spEgg.SetActive(false);

            }
            else
            {
                PlayerPrefs.SetInt("MilkCount", PlayerPrefs.GetInt("MilkCount") >= 0 ? PlayerPrefs.GetInt("MilkCount") + int.Parse(txtCropCount.text.Replace("X ", "")) : 0);
                item.GetComponent<Sheep>().milkCount = 0;
                txtCropCount.text = "X 0";
                gameObject.GetComponent<GameManagers>().LoadPlayerData();
                item.GetComponent<Sheep>().spMilk.SetActive(false);
            }
       
        }
    }

    public void UpgradeItem()
    {
        int money = PlayerPrefs.GetInt("CoinCount");
        PlayerPrefs.SetInt(gameObject.name, 0);
        if (money >= int.Parse(txtUpgradePrice.text.Replace("Upgrade : ","")))
        {
            int index = PlayerPrefs.GetInt(item.name);
            PlayerPrefs.SetInt(item.name,index+1);   
            Vector3 vector = item.transform.position;
            cropPanel.SetActive(false);
            Destroy(item);
            if(types=="C")
            {
                Instantiate(chicken[index + 1], vector, Quaternion.identity).GetComponent<Chicken>().cropManager = gameObject.GetComponent<CropManager>();
               
            }
            else
            {

                Instantiate(sheeps[index + 1], vector, Quaternion.identity).GetComponent<Sheep>().cropManager = gameObject.GetComponent<CropManager>(); ;
            }

        }
        else
        {
            Debug.Log("Yukseltme baþarýsýz");
        }
    }
    void Start()
    {
        LoadSheep("S");
        LoadSheep("C");
    }
}
