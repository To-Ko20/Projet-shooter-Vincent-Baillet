using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnnemiBulletController : MonoBehaviour
{
    private float speed = 5f;
    [SerializeField] private int id = 0;

    // Update is called once per frame
    public void GetID(int ID)
    {
        id = ID;
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
                break;
            
            case 1:
                transform.position += new Vector3(speed, -speed, 0) * Time.deltaTime;
                break;
            
            case 2:
                transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
                break;
            
            case 3:
                transform.position += new Vector3(-speed, -speed, 0) * Time.deltaTime;
                break;
        }
        

        
        
        if (transform.position.y <= -5)
        {
            Destroy(this.GameObject()); 
        }
    }
}
