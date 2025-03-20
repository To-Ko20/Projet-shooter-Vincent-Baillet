using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZipBehaviors : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int life;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject bullet;

    private int tick = 0;
    [SerializeField] private int shootTime;

    void Update()
    {
        Timer();
        Move();

        if (transform.position.y <= -5.5)
        {
            DestroyZip();
        }
    }

    void Timer()
    {
        tick++;
        if (tick >= shootTime * 60)
        {
            tick = 0;
            Shoot();
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
    
    private void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, rb.transform.position, Quaternion.identity);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void DestroyZip()
    {
        GameObject.Find("Zipzabzop Manager").GetComponent<ZipzapzopManager>().DelZip();
        Destroy(this.GameObject());
    }
}
