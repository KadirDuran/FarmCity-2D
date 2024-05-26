using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public GameObject  spMilk;
    public int milkCount = 0;
    public CropManager cropManager;
    public bool move;
    public Vector3 target;
    void Start()
    {
        
        InvokeRepeating("LayMilk", 10f, 10f);
        spMilk.SetActive(false);

        TargetCreate();
    }
    void LayMilk()
    {
        milkCount+=15;
        spMilk.SetActive(true);

    }
  

    private void OnMouseDown()
    {
        string price = PlayerPrefs.GetInt(gameObject.name) == 0 ? "500" : (PlayerPrefs.GetInt(gameObject.name) * 1000).ToString();
        cropManager.PanelInfo(milkCount.ToString(), price, "S");
        cropManager.item = gameObject;

    }


    void TargetCreate()
    {
        target = new Vector3(UnityEngine.Random.Range(9f, 25f), UnityEngine.Random.Range(-4f, 4f), -1f);
        if (math.abs(target.x) > math.abs(transform.position.x))
        {
            gameObject.transform.Rotate(0, 0, 0);
        }
        if (math.abs(target.x) < math.abs(transform.position.x))
        {
            gameObject.transform.Rotate(0, 180, 0);
        }
        move = true;
    }
    private void FixedUpdate()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 0.22f * Time.deltaTime);
            if (transform.position == target)
            {

                move = false;
                TargetCreate();
            }
        }
    }
}
