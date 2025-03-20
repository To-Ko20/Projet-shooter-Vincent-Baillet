using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    private Vector2 _movement;

    public int playerLife = 3;
    private int maxLife;

    public GameObject bullet;

    // Système de vie
    public GameObject lifeIconPrefab; // Ton prefab de sprite (coeur, etc.)
    public Transform lifePanel; // L'objet parent qui contient les icônes (par ex. un empty GameObject dans la scène)
    private List<GameObject> lifeIcons = new List<GameObject>();

    void Start()
    {
        maxLife = playerLife;
        UpdateLifeDisplay();
    }

    void Update()
    {
        _movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W) && rb.transform.position.y <= 4.5)
        {
            _movement.y = 1;
        }
        if (Input.GetKey(KeyCode.S) && rb.transform.position.y >= -4.5)
        {
            _movement.y = -1;
        }
        if (Input.GetKey(KeyCode.A) && rb.transform.position.x >= -8.5)
        {
            _movement.x = -1;
        }
        if (Input.GetKey(KeyCode.D) && rb.transform.position.x <= 8.5)
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

        if (rb != null)
        {
            rb.linearVelocity = _movement;
        }
        else
        {
            transform.position += (Vector3)_movement * Time.fixedDeltaTime;
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, rb.transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ennemibullet"))
        {
            playerLife--;
            UpdateLifeDisplay();

            Destroy(other.gameObject);

            if (playerLife <= 0)
            {
                LoseGame();
            }
        }
    }

    void LoseGame()
    {
        Debug.Log("Game Over");
        // Ici tu peux rajouter une UI de game over, etc.
    }

    private void UpdateLifeDisplay()
    {
        // On supprime toutes les icônes de la liste (réinitialisation)
        foreach (var icon in lifeIcons)
        {
            Destroy(icon);
        }

        lifeIcons.Clear();

        // On instancie autant d'icônes que la vie actuelle
        for (int i = 0; i < playerLife; i++)
        {
            GameObject icon = Instantiate(lifeIconPrefab, lifePanel);
            lifeIcons.Add(icon);
        }
    }
}
