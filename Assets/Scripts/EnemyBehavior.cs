using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    
    [SerializeField] private int maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb2d;
    
    private int health;
    
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb2d.velocity = new Vector2(-speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        rb2d.velocity = Vector2.zero;
        health--;
        //play hurt animation
    }

    public void Recover()
    {
        if(health <= 0)
        {
            Die();
        }
        rb2d.velocity = new Vector2(-speed, 0);
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
