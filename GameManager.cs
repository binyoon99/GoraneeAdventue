using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // 점수화와 스테이지 관리
    // Start is called before the first frame update

    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    //UI
    public Image[] UIHealth;
    public Text UIPoint;
    public Text UIStage ;
    public GameObject UIRestartBtn;

    // Update is called once per frame
    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();

    }
    public void NextStage()
    {
        // Change Stage
        if (stageIndex < Stages.Length - 1)
        {
            player.transform.position = new Vector3(0,0,-1);
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);

            UIStage.text = "STAGE " + (stageIndex+1);
        }
        else // Game Clear
        {
            // Plaayer Contol Lock
            Time.timeScale = 0;
            // Result UI
            Debug.Log("게임 클리어");
            // Restart Button UI
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Clear!";
            UIRestartBtn.SetActive(true);
            
        }
        // Calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;
        
    }
    public void HealthDown() {
        if (health > 1)
        {
            health--;
            UIHealth[health].color = new Color(1, 0, 0, 0.4f);

        }
        else {
            //All Health UI OFf
            UIHealth[0].color = new Color(1, 0, 0, 0.4f);
            // Player Die Effect
            player.OnDied();
            // Result UI
            Debug.Log("죽었습니다");
            // Retry Button UI
            UIRestartBtn.SetActive(true);

            // Reset Stage point and stage items
            stagePoint = 0;
            player.gameObject.SetActive(true);



        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            // Health Down
            HealthDown();

            // Player Respwan
            PlayerReposition();

            /*collision.attachedRigidbody.velocity = Vector2.zero;
            collision.transform.position = new Vector3(0, 0, -1);*/

        }
    }
    void PlayerReposition() {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();

    }

    public void Restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    
}
