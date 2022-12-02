using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject bigger_grow;
    public GameObject bigger_thinner;
    public GameObject grow_bigger;
    public GameObject grow_thinner;
    public GameObject thinner_grow;
    public GameObject thinner_bigger;
    public GameObject player;

    public GameObject ObstacleCenter2;
    public GameObject ObstacleCenter3;
    public GameObject ObstacleLeft2;
    public GameObject ObstacleLeft4;
    public GameObject ObstacleRight2;
    public GameObject ObstacleRight4;
    public GameObject ObstacleL;
    public GameObject ObstacleM;
    public GameObject ObstacleS;

    public GameObject GamePanel;
    public GameObject MenuPanel;
    public GameObject MainMenuPanel;
    public TextMeshProUGUI MuteTMP;
    public TextMeshProUGUI totalCoin;
    private bool isPaused = false;

    private float playerUpdatePos = -850;
    private float playerStartPos = -950;
    private float lastCreatedSelection = 0;
    
    private List<GameObject> selectionList = new List<GameObject>();
    private List<GameObject> obstaclesList = new List<GameObject>();

    private void Start()
    {




        totalCoin.SetText(PlayerPrefs.GetInt("Coins").ToString());
        try
        {
            int mute = PlayerPrefs.GetInt("Mute");
            if (mute == 0)
            {
                GetComponent<AudioSource>().mute = false;
                player.GetComponent<AudioSource>().mute = false;
                MuteTMP.SetText("Mute");
            }
            else if (mute == 1)
            {
                GetComponent<AudioSource>().mute = true;
                player.GetComponent<AudioSource>().mute = true;
                MuteTMP.SetText("Turn Up");
            }
        }
        catch
        {
            Debug.Log("PlayerPref Oluþturuldu");
            PlayerPrefs.SetInt("Mute", 0);
        }


        selectionList.Add(bigger_grow);
        selectionList.Add(bigger_thinner);
        selectionList.Add(grow_bigger);
        selectionList.Add(grow_thinner);
        selectionList.Add(thinner_grow);
        selectionList.Add(thinner_bigger);

        obstaclesList.Add(ObstacleCenter2);
        obstaclesList.Add(ObstacleCenter3);
        obstaclesList.Add(ObstacleLeft2);
        obstaclesList.Add(ObstacleLeft4);
        obstaclesList.Add(ObstacleRight2);
        obstaclesList.Add(ObstacleRight4);
        obstaclesList.Add(ObstacleL);
        obstaclesList.Add(ObstacleM);
        obstaclesList.Add(ObstacleS);

        for (int i = 20; i <= 200; i+=20)
        {

            int randomSelection = Random.Range(0, 6);
            int randomObstacles = Random.Range(0, 9);
            lastCreatedSelection = playerStartPos + i;
            Instantiate(selectionList[randomSelection], new Vector3(0, 1, lastCreatedSelection), Quaternion.identity);
            Instantiate(obstaclesList[randomObstacles], new Vector3(0, 0, lastCreatedSelection + 10), Quaternion.identity);
        }
    }

    private void Update()
    {
        totalCoin.SetText(PlayerPrefs.GetInt("Coins").ToString());
        if (player.transform.localPosition.z >= playerUpdatePos && player.transform.localPosition.z <= 1800)
        {
            CreateObject();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Menu();
                isPaused = true;
            }
            else
            {
                ResumeGame();
                isPaused = false;
            }
            
        }

    }

    private void CreateObject()
    {
        for (int i = 20; i <= 200; i += 10)
        {
            int randomSelection = Random.Range(0, 6);
            int randomObstacles = Random.Range(0, 9);
            lastCreatedSelection += 20;
            Instantiate(selectionList[randomSelection], new Vector3(0, 1, lastCreatedSelection), Quaternion.identity);
            Instantiate(obstaclesList[randomObstacles], new Vector3(0, 0, lastCreatedSelection + 10), Quaternion.identity);
        }
        playerUpdatePos += 200;
    }

    public void Menu()
    {
        isPaused = true;
        MenuPanel.SetActive(true);
        MenuPanel.GetComponent<Animator>().SetTrigger("FadeIn");
        player.GetComponent<CharacterController>().enabled = false;
        GamePanel.GetComponent<Animator>().SetTrigger("FadeOut");
        GamePanel.SetActive(false);
    }

    public void ResumeGame()
    {
        GamePanel.SetActive(true);
        GamePanel.GetComponent<Animator>().SetTrigger("FadeIn");
        player.GetComponent<CharacterController>().enabled = true;
        MenuPanel.GetComponent<Animator>().SetTrigger("FadeOut");
        MenuPanel.SetActive(false);
        isPaused = false;
    }

    public void Restart()
    {
        
        SceneManager.LoadScene("Game");
    }

    public void Mute()
    {
        int mute = PlayerPrefs.GetInt("Mute");
        if (mute == 0)
        {
            PlayerPrefs.SetInt("Mute", 1);
            GetComponent<AudioSource>().mute = true;
            player.GetComponent<AudioSource>().mute = true;
            MuteTMP.SetText("Turn Up");
        }
        else if (mute == 1)
        {
            PlayerPrefs.SetInt("Mute", 0);
            GetComponent<AudioSource>().mute = false;
            player.GetComponent<AudioSource>().mute = false;
            MuteTMP.SetText("Mute");
        }

    }

    public void MainMenu()
    {
        Restart();
        //MenuPanel.GetComponent<Animator>().SetTrigger("FadeOut");
        //MenuPanel.SetActive(false);
        //MainMenuPanel.SetActive(true);
        //MainMenuPanel.GetComponent<Animator>().SetTrigger("FadeIn");
        //player.GetComponent<PlayerMovement>().enabled = false;
    }

    public void PlayGame()
    {
        GamePanel.SetActive(true);
        GamePanel.GetComponent<Animator>().SetTrigger("FadeIn");
        MainMenuPanel.GetComponent<Animator>().SetTrigger("FadeOut");
        MainMenuPanel.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
    }

 

    public void Exit()
    {
        Application.Quit();
    }
}
