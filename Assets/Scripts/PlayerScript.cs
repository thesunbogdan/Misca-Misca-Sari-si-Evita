using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator animator;

    public float jumpPower = 10.0f;

    Rigidbody2D myRigidBody;

    public bool isGrounded = false;

    float posX = 0.0f;

    int scoreIdex=0;

    bool isGameOver = false;

    ChallengeController myChallengeController;

    GameController myGameController;

    AudioSource myAudioPlayer;
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip deathSound;
    public AudioClip Music;
    // Start is called before the first frame update
    void Start()
    {
        
        myRigidBody = transform.GetComponent<Rigidbody2D>();
        posX = transform.position.x;
        myChallengeController =
            GameObject.FindObjectOfType<ChallengeController>();
        myGameController = GameObject.FindObjectOfType<GameController>();
        myAudioPlayer = GameObject.FindObjectOfType<AudioSource>();
        myAudioPlayer.PlayOneShot(Music);
    }

    void FixedUpdate()
    {
        if ((Input.GetKey(KeyCode.Space)|| Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w")) && isGrounded && !isGameOver)
        {
            myRigidBody
                .AddForce(Vector3.up *
                (
                jumpPower * myRigidBody.mass * myRigidBody.gravityScale * 10.0f
                ));
            myAudioPlayer.PlayOneShot(jumpSound);
        }

        // Hit the face check
        if (transform.position.x < posX)
        {
            GameOver();
        }
    }

    void Update()
    {
        if (isGameOver) return;
        if(scoreIdex == 500)
            {
                myGameController.IncrementScore();
                scoreIdex=0;
            }
            else
            scoreIdex++;
    }

    void GameOver()
    {
        isGameOver = true;
        animator.SetFloat("Death", 1);
        myChallengeController.GameOver();
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
            animator.SetFloat("Height", 0);
        }

        if (other.collider.tag == "Enemy")
        {
            myAudioPlayer.PlayOneShot(deathSound);
            GameOver();
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = false;
            animator.SetFloat("Height", 100);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Star")
        {
            myGameController.IncrementScoreStar();
            myAudioPlayer.PlayOneShot(coinSound);
            Destroy(other.gameObject);
        }
    }
}
