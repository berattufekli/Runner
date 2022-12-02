using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    public AudioClip collect;
    public AudioClip thinner;
    public AudioClip bigger;

    public GameObject prefab;
    public TextMeshProUGUI CoincountTMP;
    public TextMeshProUGUI HeartcountTMP;

    public GameObject GameOver;

    private int canCount = 3;

    private bool hasTrigger = false;

    private int count = 1;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

   

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.CompareTag("Grow") && !hasTrigger)
        {
            audioSource.clip = collect;
            audioSource.Play();
            hasTrigger = true;
            GameObject go = Instantiate(prefab, transform.position +new Vector3(0, count * 0.13f, 0), transform.rotation) as GameObject;
            go.transform.parent = transform;
            count++;
            CoincountTMP.text = count.ToString();
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Thinner") && !hasTrigger)
        {
            audioSource.clip = thinner;
            audioSource.Play();
            other.GetComponent<AudioSource>().enabled = true;
            hasTrigger = true;
            if (gameObject.CompareTag("Bigger"))
            {
                gameObject.gameObject.GetComponent<Animator>().SetTrigger("biggerToNormal");
                gameObject.tag = "Normal";
            }
            
            else if (gameObject.CompareTag("Normal"))
            {
                gameObject.GetComponent<Animator>().SetTrigger("normalToThinner");
                gameObject.tag = "Thinner";
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Bigger") && !hasTrigger)
        {
            audioSource.clip = bigger;
            audioSource.Play();
            other.GetComponent<AudioSource>().enabled = true;
            hasTrigger = true;
            if (gameObject.CompareTag("Normal"))
            {
                gameObject.GetComponent<Animator>().SetTrigger("normalToBigger");
                gameObject.tag = "Bigger";
            }

            else if (gameObject.CompareTag("Thinner"))
            {
                gameObject.GetComponent<Animator>().SetTrigger("thinnerToNormal");
                gameObject.tag = "Normal";
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            Destroy(other.gameObject);
            canCount--;
            HeartcountTMP.SetText(canCount.ToString());
        }

        if (other.CompareTag("Selection") || other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject, 2f);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Wall"))
        {
            Destroy(hit.gameObject);
            canCount--;
            HeartcountTMP.SetText(canCount.ToString());

            if (canCount == 0)
            {
                Debug.Log("oyun bitti");
                GetComponent<PlayerMovement>().enabled = false;
                GameOver.SetActive(true);
                GameOver.GetComponent<Animator>().SetTrigger("FadeIn");
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        hasTrigger = false;
    }
}
