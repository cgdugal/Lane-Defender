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
    [SerializeField] private Transform bulletSpawnPosition;

    private bool isMoving;
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
        Restart.started += Restart_started;
        Quit.started += Quit_started;
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
        //play explosion animated
        Instantiate(bullet, bulletSpawnPosition.transform.position, Quaternion.identity);
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
        rb2d.velocity = new Vector2(0, moveDirection * moveSpeed);
    }

    private void OnDestroy()
    {
        Move.started -= Move_started;
        Move.canceled -= Move_canceled;
        Shoot.started -= Shoot_started;
        Restart.started -= Restart_started;
        Quit.started -= Quit_started;
    }


}
