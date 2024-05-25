using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public GameObject egg,spEgg;
    public int eggCount = 0;
    public CropManager cropManager;
    public bool move;
    public Vector3 target;
    void Start()
    {
        InvokeRepeating("LayEggs",10f,10f);
          spEgg.SetActive(false);
       
        TargetCreate();
    }
    void LayEggs()
    {
        eggCount++;
        spEgg.SetActive(true);

    }

    private void OnMouseDown()
    {
        string price = PlayerPrefs.GetInt(gameObject.name) == 0 ? "500" : (PlayerPrefs.GetInt(gameObject.name) * 500).ToString();
        cropManager.PanelInfo(eggCount.ToString(), price,"C");
        cropManager.item = gameObject;
       
    }

   
    void TargetCreate()
    {
        target = new Vector3(UnityEngine.Random.Range(-26f, -9.1f), UnityEngine.Random.Range(-4.8f, 4.8f), -1f);
        if (math.abs(target.x) < math.abs(transform.position.x))
        {
            gameObject.transform.Rotate(0,0,0);
        }
        if (math.abs(target.x) > math.abs(transform.position.x))
        {
            gameObject.transform.Rotate(0, 180, 0);
        }
        move = true;
    }
    private void FixedUpdate()
    {
        if(move)
        {
            transform.position = Vector3.MoveTowards(transform.position,target,0.22f*Time.deltaTime);
            if(transform.position == target)
            {
               
                move = false;
                TargetCreate();
            }
        }
    }
}
