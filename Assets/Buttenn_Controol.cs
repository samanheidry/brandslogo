using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttenn_Controol : MonoBehaviour
{
    public string BrandName;

    public Controll Controll;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   public  void OnClick()
    {
        Controll.CheckSelectedBrand(BrandName);
    }
}
