
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject RestartBtn;

    private void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }
    public void Nextstage()
    {
        if(stageIndex<Stages.Length)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE" + (stageIndex+1);
        }
        else
        {
            Time.timeScale = 0;
            Debug.Log("°ÔÀÓÅ¬");
            RestartBtn.SetActive(true);
            Text btnText = RestartBtn.GetComponentInChildren<Text>();
            btnText.text = " Clear!";
            RestartBtn.SetActive(true);
        }
        totalPoint += stagePoint;
        stagePoint = 0;
    }
    public void HealthDown()
    {
        if(health>1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            player.Ondie();
            Debug.Log("die");
            RestartBtn.SetActive(true);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {

            if (health > 0)
            {
                PlayerReposition();   
            }
            HealthDown();
        }

    }
    void PlayerReposition()
    {
        player.VelocityZero();
        player.transform.position = new Vector3(1.95f, 2.78f, 1);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
