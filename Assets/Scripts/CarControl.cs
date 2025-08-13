using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float currentSpeed;
    public float reverseSpeed = 3f;
    public float turnSpeed = 1000f;
    private float nextTurnTime = 0f;
    public float carTurnDelay = 0.2f;

    private bool inSlowZone = false;
    private bool inTombZone = false;
    private bool isMoving = false;

    [SerializeField] private AudioClip gremlinScream;
    [SerializeField] private AudioSource sfxSource; //dedicated one-shot source

    [SerializeField] private AudioSource idleSource;
    [SerializeField] private AudioSource revSource;




    Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        UpdateSpeed();
        EngineGoBrr();
    }

    void UpdateSpeed()
    {
        if (inTombZone)
            currentSpeed = moveSpeed * 0.3f;
        else if (inSlowZone)
            currentSpeed = moveSpeed * 0.4f;
        else
            currentSpeed = moveSpeed;
    }

    void CheckInput()
    {

        isMoving = false;

        if (Time.time > nextTurnTime)
        {
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(new Vector3(0, 0, turnSpeed * Time.deltaTime));
            }
            if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(new Vector3(0, 0, -turnSpeed * Time.deltaTime));
            }
            nextTurnTime = Time.time+carTurnDelay;
        }
            
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            isMoving = true;
            transform.Translate(Vector2.up * currentSpeed * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            isMoving = true;
            transform.Translate(Vector2.down * reverseSpeed * .08f * Time.deltaTime, Space.Self);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("SlowZone"))
        {
            inSlowZone = true;
        }


        if (collision.CompareTag("Gremlin"))
        {
            inTombZone = true;
            if (sfxSource && gremlinScream )
            {
                PlaySFX(gremlinScream); 
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SlowZone"))
        {
            inSlowZone = false;
        }
        if (collision.CompareTag("TombStone"))
        {
            inTombZone = false;

        }

    }

    void EngineGoBrr()
    {
        if (isMoving)
        {
            if (!revSource.isPlaying) revSource.Play();
            if (idleSource.isPlaying) idleSource.Stop();
        }
        else
        {
            if (!idleSource.isPlaying) idleSource.Play();
            if (revSource.isPlaying) revSource.Stop();
        }
    }


    private void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
    }

}
