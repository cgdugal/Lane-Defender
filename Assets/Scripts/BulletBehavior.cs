using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    
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
            collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage();
        }
        //play explosion animation
        Destroy(this.gameObject);

    }
}
