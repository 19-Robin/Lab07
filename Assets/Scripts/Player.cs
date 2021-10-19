using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animation thisAnimation;
    private Rigidbody rigid;
    public float addforce;
    public static bool counter;
    public GameObject[] pillar;
    public AudioClip[] audioClip;
    public AudioSource audioSource;

    void Start()
    {
        counter = false;
        audioSource = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();
        thisAnimation = GetComponent<Animation>();
        thisAnimation["Flap_Legacy"].speed = 3;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& transform.position.y >= -4.325f)
        {
            audioSource.PlayOneShot(audioClip[1]);
            thisAnimation.Play();
            rigid.AddForce(transform.up * addforce, ForceMode.Impulse);
        }
        if (transform.position.y >= 3.33f)
        {
            transform.position = new Vector3(transform.position.x, 3.33f, transform.position.z);
        }
        if (transform.position.y <= -4.325f)
        {
            SceneManager.LoadScene("GameOverScene");
        }

        pillar = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject p in pillar)
        {
            if (p.transform.position.x < transform.position.x && counter == false)
            {
                audioSource.PlayOneShot(audioClip[0]);
                GameManager.thisManager.UpdateScore(1);
                counter = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
