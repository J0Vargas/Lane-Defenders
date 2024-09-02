using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private PlayerInput _playerinput;
    [SerializeField] private float speed = 10;
    [SerializeField] private GameObject GameManager;
    [SerializeField] private LaunchBullet LB;
    [SerializeField] private AudioClip TankShoot;
    [SerializeField] private AudioSource Tankshot;
    [SerializeField] private AudioClip Explosion1;
    [SerializeField] private AudioSource Exploded;
    [SerializeField] private AudioClip LifeD;
    [SerializeField] private AudioSource LiveD;
    [SerializeField] private int score = 0;
    [SerializeField] private TMP_Text scoretext;
    [SerializeField] private int lives = 3;
    [SerializeField] private TMP_Text livestext;
    [SerializeField] private GameObject Explosion;

    [SerializeField] private GameObject Barrel;

    private InputAction Movement;
    private InputAction Quit;
    private InputAction Restart;
    private InputAction Fire;

    private bool HasBeenPressed;
    private bool isPlayerMoving;
    private bool HasBeenLaunched;
    private float moveDirection;
    private bool IsLeft;
    private bool IsRight;
    private float timer;

    private Coroutine CurrentTimer;
    // Start is called before the first frame update
    void Start()
    {
        _playerinput.currentActionMap.Enable();
        Movement = _playerinput.currentActionMap.FindAction("Movement");
        Quit = _playerinput.currentActionMap.FindAction("Quit");
        Restart = _playerinput.currentActionMap.FindAction("Restart");
        Fire = _playerinput.currentActionMap.FindAction("Fire");

        Movement.started += Movement_Started;
        Movement.canceled += Movement_Canceled;
        Quit.started += Quit_Started;
        Restart.started += Restart_Started;
        Fire.started += Fire_Started;
        Fire.canceled += Fire_Canceled;
        scoretext.text ="Score:" + score.ToString();
        livestext.text = "Lives:" + lives.ToString();

        GameManager = GameObject.Find("GameManager");
        LB = GameManager.GetComponent<LaunchBullet>();
    }

    private void Fire_Canceled(InputAction.CallbackContext context)
    {
       HasBeenPressed = false;
    }

    /// <summary>
    /// will stop the tanks movment when no button is being held
    /// </summary>
    /// <param name="context"></param>
    private void Movement_Canceled(InputAction.CallbackContext context)
    {
        isPlayerMoving = false;
    }

    private void Fire_Started(InputAction.CallbackContext context)
    {
        HasBeenPressed = true;
        LB.FireBullet(IsLeft, IsRight);
        AudioSource.PlayClipAtPoint(TankShoot, transform.position);
        Instantiate(Explosion, Barrel.transform.position, Quaternion.identity);
        StartCoroutine(BulletTime());
    }

    private IEnumerator BulletTime()
    {
        LB.FireBullet(false, true);
        timer = 2f;

        Instantiate(Explosion, Barrel.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(TankShoot, transform.position);
        while (HasBeenPressed == true || timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0 && HasBeenPressed == true)
            {
                LB.FireBullet(false, true);
                timer = 2f;

                Instantiate(Explosion, Barrel.transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(TankShoot, transform.position);
            }
            yield return null;
        }
        timer = 2f;

        CurrentTimer = null;
    }

    private void Restart_Started(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  
    private void Quit_Started(InputAction.CallbackContext context)
    {

        Application.Quit();
    }
    /// <summary>
    /// if isPlayerMoving is true the tank will move
    /// </summary>
    /// <param name="context"></param>
    private void Movement_Started(InputAction.CallbackContext context)
    {
        isPlayerMoving = true;
    }
    /// <summary>
    /// this checks to see if the tank is supposed to be moving if it is the tank will move in the direction that's 
    /// being pressed otherwise it'll stay in place
    /// </summary>
    private void FixedUpdate()
    {
        if(isPlayerMoving)
        {
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed * moveDirection);
            moveDirection = Movement.ReadValue<float>();

            if (moveDirection == -1)
            {
                IsLeft = true;
                IsRight = false;

            }
            if (moveDirection == 1)
            {
                IsLeft = false;
                IsRight = true;

            }
        }
        else
        {
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerMoving)
        {
            moveDirection = Movement.ReadValue<float>();
        }
    }
    /// <summary>
    /// updates the score by 100 points
    /// </summary>
    public void UpdateScore()
    {
        score += 100;
        scoretext.text = "Score:" + score.ToString();
    }
    /// <summary>
    /// subtracts 1 life
    /// </summary>
    public void LoseALife()
    {
        lives--;               
        livestext.text = "Lives:" + lives.ToString();
        AudioSource.PlayClipAtPoint(LifeD, transform.position);
        if (lives == 0)
        {

        }
    }
}
