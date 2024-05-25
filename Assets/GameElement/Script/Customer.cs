using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Transform stand;
    public GameObject orderPanel;
    public TextMeshProUGUI txtEgg, txtMilk; 
    //public float waitTime = 0.0f;Bekledikçe sinirlenebilir? 
    void CreateOrder()
    {

    }
    void ComeBack()
    {
      
    }
    void ShowOrder()
    {
        CreateOrder();
        orderPanel.SetActive(!orderPanel.activeSelf);
        txtEgg.text = "  x Egg ";
        txtMilk.text = "  x Milk ";
    }
    void OnMouseDown()
    {
        ShowOrder();
    }



    void Update()
    {
        float distance = Vector2.Distance(stand.position, transform.position);
        if(distance > 1.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, stand.position, 0.01f);
        }
    }
}
