using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZipzapzopManager : MonoBehaviour
{
    public List<GameObject> zipType;
    [SerializeField] private int zipsCount = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateZip(1);
    }
    
    void CreateZip(int nb)
    {
        for(int i = 0; i < nb; i++)
        {
            Instantiate(zipType[0], new Vector2(Random.Range(-8, 8), 4.5f), Quaternion.identity);
            zipsCount++;
        }
    }

    public void DelZip()
    {
        zipsCount -= 1;
        if (zipsCount <= 0)
        {
            CreateZip(Random.Range(1, 4));
        }
    }
}
