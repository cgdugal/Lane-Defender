using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private AudioSource hitSound;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            hitSound.Play();
            collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage();
        }
        Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);

    }
}
