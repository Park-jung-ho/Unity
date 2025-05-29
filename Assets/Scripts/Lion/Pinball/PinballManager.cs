using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PinballManager : MonoBehaviour
{
    public int score;
    public float BarPower;
    public GameObject pinball;
    public TMP_Text scoreText;
    public Button startButton;
    public Rigidbody2D leftBarRb;
    public Rigidbody2D rightBarRb;
    

    void Update()
    {
        scoreText.text = score.ToString();
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            leftBarRb.AddTorque(BarPower);
        }
        else
        {
            leftBarRb.AddTorque(-BarPower);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rightBarRb.AddTorque(-BarPower);
        }
        else
        {
            rightBarRb.AddTorque(BarPower);
        }
    }

    public void startGame()
    {
        pinball.transform.position = new Vector3(8f, 5f, 0f);
        score = 0;
        pinball.SetActive(true);
    }

    public void GameOver()
    {
        pinball.SetActive(false);
        startButton.interactable = true;
    }
}
