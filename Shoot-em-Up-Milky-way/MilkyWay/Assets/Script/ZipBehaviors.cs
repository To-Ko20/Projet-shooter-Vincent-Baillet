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
    
    [SerializeField] private int zipID;

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
        if(other.gameObject.tag.Equals("bullet") && life <= 0)
        {
            DestroyZip();
            Destroy(other.GameObject());
        }
        else if (other.gameObject.tag.Equals("bullet"))
        {
            life--;
            Destroy(other.GameObject());
        }
    }
    
    private void Shoot()
    {
        GameObject newBullet;
        EnnemiBulletController bulletScript;

        switch (zipID)
        {
            case 0:
                newBullet = Instantiate(bullet, rb.transform.position, Quaternion.identity);
                bulletScript = newBullet.GetComponent<EnnemiBulletController>();

                if (bulletScript != null)
                {
                    bulletScript.Init(zipID, Vector3.zero); // pas besoin de target ici
                }
                break;

            case 1:
                // Multi-shoot : centre, droite, gauche
                newBullet = Instantiate(bullet, rb.transform.position, Quaternion.identity);
                bulletScript = newBullet.GetComponent<EnnemiBulletController>();
                if (bulletScript != null) bulletScript.Init(zipID, Vector3.zero);

                newBullet = Instantiate(bullet, rb.transform.position, Quaternion.identity);
                bulletScript = newBullet.GetComponent<EnnemiBulletController>();
                if (bulletScript != null) bulletScript.Init(0, Vector3.zero);

                newBullet = Instantiate(bullet, rb.transform.position, Quaternion.identity);
                bulletScript = newBullet.GetComponent<EnnemiBulletController>();
                if (bulletScript != null) bulletScript.Init(3, Vector3.zero);
                break;

            case 2:
                newBullet = Instantiate(bullet, rb.transform.position, Quaternion.identity);
                bulletScript = newBullet.GetComponent<EnnemiBulletController>();

                if (bulletScript != null)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        bulletScript.Init(2, player.transform.position);
                    }
                    else
                    {
                        Debug.LogWarning("Player not found for targeted bullet.");
                    }
                }
                break;
        }
    }



    // ReSharper disable Unity.PerformanceAnalysis
    private void DestroyZip()
    {
        GameObject.Find("Zipzabzop Manager").GetComponent<ZipzapzopManager>().DelZip();
        Destroy(this.GameObject());
    }
}
