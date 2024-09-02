using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    
    [SerializeField] private int maxHealth;
    [SerializeField] private int points;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource deathSound;

    [SerializeField] private GameManager gm;
    
    private int health;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb2d.velocity = new Vector2(-speed, 0);
        gm = FindObjectOfType<GameManager>();
        gm.enemies.Add(this);
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
        animator.SetTrigger("Hit");
        if(health <= 0)
        {
            animator.SetBool("Dead", true);
            deathSound.Play();
        } 
    }

    public void Recover()
    {
        rb2d.velocity = new Vector2(-speed, 0);
    }

    public void Die()
    {
        gm.AddScore(points);
        Destroy(this.gameObject);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Bullet")
        {
            gm.RemoveLife();
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        gm.enemies.Remove(this);
    }

    public void Freeze()
    {
        rb2d.velocity = Vector2.zero;
    }
}
