using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float jumpPower = 10.0f;

    Rigidbody2D myRigidBody;

    public bool isGrounded = false;

    float posX = 0.0f;

    bool isGameOver = false;

    ChallengeController myChallengeController;

    GameController myGameController;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = transform.GetComponent<Rigidbody2D>();
        posX = transform.position.x;
        myChallengeController =
            GameObject.FindObjectOfType<ChallengeController>();
        myGameController = GameObject.FindObjectOfType<GameController>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded && !isGameOver)
        {
            myRigidBody
                .AddForce(Vector3.up *
                (
                jumpPower * myRigidBody.mass * myRigidBody.gravityScale * 10.0f
                ));
        }

        // Hit the face check
        if (transform.position.x < posX)
        {
            GameOver();
        }
    }

    void Update()
    {
    }

    void GameOver()
    {
        isGameOver = true;
        myChallengeController.GameOver();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
        }

        if (other.collider.tag == "Enemy")
        {
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
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Star")
        {
            myGameController.IncrementScore();
            Destroy(other.gameObject);
        }
    }
}
