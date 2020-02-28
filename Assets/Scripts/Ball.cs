
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle paddle;
    [SerializeField] private float xPush = 2f;
    [SerializeField] private float yPush = 15f;
    [SerializeField] private AudioClip[] ballSounds;
    [SerializeField] private float randomFactory = 0.2f;

    private bool hasStarted;
    Vector2 paddleToBallVector;
    AudioSource audioSource;
    Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        hasStarted = false;
        paddleToBallVector = transform.position - paddle.transform.position;
        audioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidbody2D .velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactory), Random.Range(0f, randomFactory));
        if (hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            audioSource.PlayOneShot(clip);
            myRigidbody2D.velocity += velocityTweak;
        }
    }
}
