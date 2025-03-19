using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Définir une vitesse par défaut
    public Rigidbody2D rb;
    private Vector2 _movement;

    public GameObject bullet;

    void Update()
    {
        _movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W) && rb.transform.position.y <= 4.5) // Avancer
        {
            _movement.y = 1;
        }
        if (Input.GetKey(KeyCode.S) && rb.transform.position.y >= -4.5) // Reculer
        {
            _movement.y = -1;
        }
        if (Input.GetKey(KeyCode.A) && rb.transform.position.x >= -8.5) // Gauche
        {
            _movement.x = -1;
        }
        if (Input.GetKey(KeyCode.D) && rb.transform.position.x <= 8.5) // Droite
        {
            _movement.x = 1;
        }
        
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Move()
    {
        _movement = _movement.normalized * speed;
        
        if (rb is not null)
        {
            rb.linearVelocity = _movement;
        }
        else
        {
            transform.position += (Vector3)_movement * Time.fixedDeltaTime;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, rb.transform.position, Quaternion.identity);
    }
}