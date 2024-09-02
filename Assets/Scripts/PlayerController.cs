using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInputInstance;
    [SerializeField] private Rigidbody2D rb2d;

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private Transform bulletSpawnPosition;

    [SerializeField] private float shootDelay;

    [SerializeField] AudioSource shootSound;

    private bool isMoving;
    private bool isShooting;
    private float moveDirection;

    private InputAction Move;
    private InputAction Shoot;
    private InputAction Restart;
    private InputAction Quit;
    
    // Start is called before the first frame update
    void Start()
    {
        playerInputInstance.currentActionMap.Enable();

        Move = playerInputInstance.currentActionMap.FindAction("Move");
        Shoot = playerInputInstance.currentActionMap.FindAction("Shoot");
        Restart = playerInputInstance.currentActionMap.FindAction("Restart");
        Quit = playerInputInstance.currentActionMap.FindAction("Quit");

        Move.started += Move_started;
        Move.canceled += Move_canceled;
        Shoot.started += Shoot_started;
        Shoot.canceled += Shoot_canceled;
        Restart.started += Restart_started;
        Quit.started += Quit_started;

        StartCoroutine(ShootBullet());
    }



    private void Quit_started(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    private void Restart_started(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Shoot_started(InputAction.CallbackContext obj)
    {
        isShooting = true;
    }

    private void Shoot_canceled(InputAction.CallbackContext obj)
    {
        isShooting = false;
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        isMoving = false;
    }

    private void Move_started(InputAction.CallbackContext obj)
    {
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            moveDirection = Move.ReadValue<float>();
        }
        else
        {
            moveDirection = 0;
        }
        
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb2d.velocity = new Vector2(0, moveDirection * moveSpeed);
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
        
    }

    public void RemoveControl()
    {
        Move.started -= Move_started;
        Move.canceled -= Move_canceled;
        Shoot.started -= Shoot_started;
        Shoot.canceled -= Shoot_canceled;
        isMoving = false;
        isShooting = false;
    }

    private void OnDestroy()
    {
        RemoveControl();
        Restart.started -= Restart_started;
        Quit.started -= Quit_started;
    }

    IEnumerator ShootBullet()
    {
        while (true)
        {
            if (isShooting)
            {
                shootSound.Play();
                Instantiate(explosionEffect, bulletSpawnPosition.transform.position, Quaternion.identity);
                Instantiate(bullet, bulletSpawnPosition.transform.position, Quaternion.identity);

                yield return new WaitForSeconds(shootDelay);
            }

            yield return null; 
        }
    }
}

