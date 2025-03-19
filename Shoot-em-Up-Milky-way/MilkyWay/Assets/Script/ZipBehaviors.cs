using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZipBehaviors : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int life;

    void Update()
    {
        Move();

        if (transform.position.y <= -5.5)
        {
            DestroyZip();
        }
    }

    void Move()
    {
        Vector2 movement = default;
        movement.y = -1;
        movement = movement.normalized * speed;
        transform.position += (Vector3)movement * Time.deltaTime;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        life--;
        if(other.gameObject.tag.Equals("bullet") && life <= 0)
        {
            DestroyZip();
            Destroy(other.GameObject());
        }
        else if (other.gameObject.tag.Equals("bullet"))
        {
            Destroy(other.GameObject());
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void DestroyZip()
    {
        GameObject.Find("Zipzabzop Manager").GetComponent<ZipzapzopManager>().DelZip();
        Destroy(this.GameObject());
    }
}
